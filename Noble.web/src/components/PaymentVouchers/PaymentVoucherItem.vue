<template>
    <modal show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">

                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row" v-if="isAging">
                    <div class="col-lg-4 col-md-4 col-sm-6">
                        <label>
                            {{ $t('AddPaymentVoucher.Amount') }} :
                            <span class="text-danger"> *</span>
                        </label>
                        <div class="form-group">
                            <input class="form-control" type="number" v-model="amount" @click="$event.target.select()" @update:modelValue="GetValue" />
                        </div>
                    </div>
                </div>

                <div v-bind:class="{ 'horizontal-table': tableLength >= cardLength }">
                    <div class="  mt-4 ">
                        <table class="table">
                            <thead class="thead-light table-hover">
                                <tr class="text-capitalize text-center">
                                    <th style="width: 5%;" class="text-center">
                                        #
                                    </th>
                                    <th style="width:20%;" class="text-center">Date</th>
                                    <th style="width:25%;" class="text-center">Invoice No</th>
                                    <th style="width:15%;" class="text-center">Total Invoice Amount</th>
                                    <th style="width:10%;" class="text-center">Due Amount</th>
                                    <th style="width:15%;">Pay Amount</th>
                                    <th style="width:15%;">Remaining</th>
                                    <th style="width:5%;" class="text-center" v-if="!isAging">{{ $t('PurchaseBillItem.Action') }}</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(expense,index) in paymentVoucherItem" v-bind:key="expense.id" v-bind:class="{'colorDiv':expense.isActive}">
                                    <td class="text-center">{{index+1}}</td>
                                    <td class="text-center">
                                        <span class="badge badge-soft-primary"> {{ expense.compareDate }} Day</span>    {{ expense.date }}
                                    </td>
                                    <td class="text-center">
                                        {{ expense.description }} {{expense.partiallyInvoices==1?'Un-Paid': expense.partiallyInvoices==2?'Partially Paid':'Fully Paid' }}
                                        <!-- <input readonly class="form-control " v-model="expense.description" /> -->
                                        <!-- <span style="position: absolute;top: 15px;right: 79px;" class="badge bg-danger" v-if="expense.partiallyInvoices==1">
                                            Un-Paid
                                        </span>
                                        <span style="position: absolute;top: 15px;right: 79px;" class="badge bg-primary" v-else-if="expense.partiallyInvoices==2">
                                            Partially Paid
                                        </span>
                                        <span style="position: absolute;top: 15px;right: 79px;" class="badge bg-success" v-else>

                                            Fully Paid
                                        </span> -->

                                    </td>

                                    <td class="text-center">
                                        {{ expense.amount }}
                                    </td>
                                    <td class="text-center" style="color:red !important">
                                        {{ expense.dueAmount }}
                                    </td>
                                    <td class="text-center">
                                        <input type="number" @click="$event.target.select()" v-bind:disabled="isAging" @update:modelValue="updateLineTotal(expense.payAmount, 'payAmount', expense)" class="form-control text-center" v-model="expense.payAmount" />
                                    </td>
                                    <td class="text-center">
                                        {{ expense.total }}
                                    </td>
                                    <td class="text-center" v-if="!isAging">
                                        <button title="Add New Item" class="btn  btn-sm btn-round btn-icon float-right  " @click="updateLineTotal(expense.payAmount, 'isActive', expense)">
                                            <i class="fa fa-check"></i>
                                        </button>
                                    </td>

                                </tr>

                                <tr style="border:none !important;" v-if="paymentVoucherItem.length">
                                    <td colspan="5">
                                        <h6 class="m-0 text-left"> &nbsp;</h6>
                                    </td>
                                    <td class="text-center">
                                        {{payAmount}}
                                    </td>
                                    <td class="text-center">
                                        {{totalRemaining}}
                                    </td>

                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <h6 class="m-0 text-left"> &nbsp;</h6>
                                    </td>
                                    <td class="text-end  fw-bold ">
                                        Extra Amount
                                    </td>
                                    <td class="text-end fw-bold ">
                                        {{currency}} {{$roundOffFilter(total)  }}
                                    </td>

                                </tr>

                            </tbody>

                        </table>

                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled=" v$.amount.$invalid ||  !paymentVoucherItem.some(x => x.isActive) " v-on:click="SaveColor">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
        </div>

    </modal>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";

    import {
        minValue
    } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';

    export default {
        mixins: [clickMixin],
        props: ['isDisable', 'show', 'newpaymentVoucherItems', 'paymentVoucheramount', 'isSaleInvoice', 'paymentVoucher', 'isPurchaseInvoice'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                paymentVoucherItems: this.newpaymentVoucherItems,
                loading: false,
                isUpdate: false,
                isAging: true,
                isService: false,
                refresh: 0,
                paymentVoucherItem: [],

                currentItem: {
                    id: '',
                    isActive: true,
                    isAging: true,
                    date: '',
                    compareDate: '',
                    amount: 0.00,
                    extraAmount: 0.00,
                    dueAmount: 0.00,
                    total: 0.00,
                    payAmount: 0.00,
                    description: ''
                },
                addItem: false,
                IsExpenseAccount: false,
                tableLength: 0,
                cardLength: 0,
                total: 0,
                amount: 0,
                currency: '',
            }
        },
        validations() {

            return {
                amount: {
                    minValue: minValue(0.01)
                }
                }

        },
        filters: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },
        computed: {

            totalRemaining: function () {
                return this.paymentVoucherItem.reduce(function (a, c) {

                    return a + Number((c.isActive == true ? c.total < 0 ? c.total * -1 : c.total : 0) || 0)

                }, 0)
            },
            payAmount: function () {
                return this.paymentVoucherItem.reduce(function (a, c) {

                    return a + Number((c.isActive == true ? c.payAmount < 0 ? c.payAmount * -1 : c.payAmount : 0) || 0)

                }, 0)
            },
        },

        methods: {
            updateLineTotal: function (e, prop, expense) {

                if (prop == "payAmount") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    } else {
                        expense.payAmount = e;
                    }
                }
                if (prop == "isActive") {
                    if (expense.isActive) {
                        expense.isActive = false;
                    } else {
                        expense.isActive = true;
                    }

                }

                if (expense.isActive) {
                    expense.total = (parseFloat(expense.dueAmount) - expense.payAmount);
                    if (expense.total < 0) {
                        expense.extraAmount = expense.total;
                        expense.total = 0;
                    } else {
                        expense.extraAmount = 0
                    }

                }
                this.paymentVoucherItem['expense'] = expense

                if (!this.isAging) {

                    this.amount = this.paymentVoucherItem.reduce(function (a, c) {

                        return a + Number((c.isActive == true ? c.payAmount < 0 ? c.payAmount * -1 : c.payAmount : 0) || 0)

                    }, 0)
                    this.total = this.paymentVoucherItem.reduce(function (a, c) {

                        return a + Number((c.isActive == true ? c.extraAmount < 0 ? c.extraAmount * -1 : c.extraAmount : 0) || 0)

                    }, 0)
                }
                //this.$emit("update:modelValue", this.purchaseProducts);

            },
            SaveColor: function () {

                document.querySelector("html").classList.remove("perfect-scrollbar-on");

                this.$emit('updatedailyExpenseRows', this.paymentVoucherItem, this.amount);

            },
            close: function () {
                this.$emit('close');
            },
            GetValue: function () {
                {
                    var root = this;
                    if (this.amount > 0) {
                        if (this.isSaleInvoice) {
                            if (this.amount == root.paymentVoucher.amount) {
                                root.total = parseFloat(root.amount);
                                this.paymentVoucherItem.forEach(function (paymentVouchers) {
                                    if (root.amount > 0 && root.total > 0) {
                                        if (paymentVouchers.saleInvoiceId == root.paymentVoucher.saleInvoice) {
                                            paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                            root.total = root.total - paymentVouchers.payAmount;
                                            paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                            paymentVouchers.isActive = true;
                                        }
                                    }
                                });
                            }
                            else {
                                root.total = parseFloat(root.amount);

                                this.paymentVoucherItem.forEach(function (paymentVouchers) {
                                    if (root.amount > 0 && root.total > 0) {

                                        if (root.total <= root.paymentVoucher.amount) {
                                            if (paymentVouchers.saleInvoiceId == root.paymentVoucher.saleInvoice) {
                                                paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                                root.total = root.total - paymentVouchers.payAmount;
                                                paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                                paymentVouchers.isActive = true;
                                            }
                                            else {
                                                paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                                root.total = root.total - paymentVouchers.payAmount;
                                                paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                                paymentVouchers.isActive = true;
                                            }
                                        }
                                        else {
                                            paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                            root.total = root.total - paymentVouchers.payAmount;
                                            paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                            paymentVouchers.isActive = true;
                                        }
                                    }
                                    else {
                                        paymentVouchers.payAmount = 0
                                        paymentVouchers.total = 0;
                                        paymentVouchers.isActive = false;
                                    }
                                });
                            }
                        }
                        else if (root.isPurchaseInvoice) {

                            if (this.amount == root.paymentVoucher.amount) {
                                root.total = parseFloat(root.amount);
                                this.paymentVoucherItem.forEach(function (paymentVouchers) {
                                    if (root.amount > 0 && root.total > 0) {

                                        if (paymentVouchers.purchaseInvoiceId == root.paymentVoucher.purchaseInvoice) {
                                            paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                            root.total = root.total - paymentVouchers.payAmount;
                                            paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                            paymentVouchers.isActive = true;
                                        }
                                    }
                                });
                            }
                            else {
                                root.total = parseFloat(root.amount);

                                this.paymentVoucherItem.forEach(function (paymentVouchers) {
                                    if (root.amount > 0 && root.total > 0) {

                                        if (root.total == root.paymentVoucher.amount) {
                                            if (paymentVouchers.purchaseInvoiceId == root.paymentVoucher.purchaseInvoice) {
                                                paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                                root.total = root.total - paymentVouchers.payAmount;
                                                paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                                paymentVouchers.isActive = true;
                                            }
                                            else {
                                                paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                                root.total = root.total - paymentVouchers.payAmount;
                                                paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                                paymentVouchers.isActive = true;
                                            }
                                        }
                                        else {
                                            paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                            root.total = root.total - paymentVouchers.payAmount;
                                            paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                            paymentVouchers.isActive = true;
                                        }
                                    }
                                    else {
                                        paymentVouchers.payAmount = 0
                                        paymentVouchers.total = 0;
                                        paymentVouchers.isActive = false;
                                    }
                                });
                            }
                        }
                        else {
                            root.total = parseFloat(root.amount);
                            this.paymentVoucherItem.forEach(function (paymentVouchers) {
                                if (root.amount > 0 && root.total > 0) {
                                    paymentVouchers.payAmount = root.total - parseFloat(paymentVouchers.dueAmount) >= 0 ? paymentVouchers.dueAmount : root.total;
                                    root.total = root.total - paymentVouchers.payAmount;
                                    paymentVouchers.total = parseFloat(paymentVouchers.dueAmount) - paymentVouchers.payAmount;
                                    paymentVouchers.isActive = true;
                                } else {
                                    paymentVouchers.payAmount = 0
                                    paymentVouchers.total = 0;
                                    paymentVouchers.isActive = false;
                                }
                            });
                        }
                    }
                    else {
                        this.paymentVoucherItem.forEach(function (paymentVouchers) {
                            {
                                paymentVouchers.isActive = false;
                                paymentVouchers.payAmount = 0;
                                root.total = 0;
                                paymentVouchers.total = 0;
                            }
                        });
                    }

                }
            },

            checkTableWidth: function () {
                if (document.getElementsByClassName('itemtable')[0] != undefined) {
                    this.tableLength = document.getElementsByClassName('itemtable')[0].clientWidth;
                    this.cardLength = document.getElementsByClassName('card')[0].clientWidth - 25;
                }
            },

            addDailyExpense: function () {

                this.loading = true;
                this.paymentVoucherItem.push({
                    id: this.createUUID(),
                    amount: this.paymentVouchers.amount,
                    description: this.paymentVouchers.description,
                    date: this.paymentVouchers.date,
                    compareDate: this.paymentVouchers.compareDate,
                    isActive: true,
                    isAging: true,

                });

                this.paymentVouchers = {
                    id: '',
                    amount: 0.00,
                    extraAmount: 0.00,
                    date: '',
                    compareDate: '',
                    description: '',
                    isActive: true,
                    isAging: true,

                };
                this.refresh += 1;
            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },

            removeExpense: function (id) {

                var ds = this.paymentVoucherItem.findIndex(function (i) {
                    return i.id === id;
                });

                this.paymentVoucherItem.splice(ds, 1);
            },
        },
        created: function () {
            this.amount = this.paymentVoucheramount;

            if (this.isSaleInvoice) {
                const indexToMove = this.paymentVoucherItems.findIndex(item => item.saleInvoiceId === this.paymentVoucher.saleInvoice);
                if (indexToMove !== -1) {
                    const itemToMove = this.paymentVoucherItems.splice(indexToMove, 1)[0];
                    this.paymentVoucherItems.unshift(itemToMove);
                }
            }
            else if (this.isPurchaseInvoice) {
                const indexToMove = this.paymentVoucherItems.findIndex(item => item.purchaseInvoiceId === this.paymentVoucher.purchaseInvoice);
                if (indexToMove !== -1) {
                    const itemToMove = this.paymentVoucherItems.splice(indexToMove, 1)[0];
                    this.paymentVoucherItems.unshift(itemToMove);
                }
            }
            if (this.isValid('MultiplePayment')) {
                if (this.isValid('UnFollowAging')) {
                    this.isAging = false;
                }

            }

        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
            this.isService = localStorage.getItem('IsService') == 'true' ? true : false;
            var root = this;
            if (this.paymentVoucherItems.length > 0) {
                this.paymentVoucherItem = [];
                this.paymentVoucherItems.forEach(function (result) {


                    if (result != undefined) {
                        root.paymentVoucherItem.push({
                            saleInvoiceId: result.saleInvoiceId,
                            purchaseInvoiceId: result.purchaseInvoiceId,
                            amount: result.amount,
                            payAmount: result.payAmount,
                            isAging: result.isAging,
                            isActive: result.isActive,
                            total: result.total,
                            extraAmount: result.extraAmount,
                            description: result.description,
                            dueAmount: result.dueAmount,
                            receivedAmount: result.receivedAmount,
                            partiallyInvoices: result.partiallyInvoices,
                            date: moment(result.date).format('LLL'),
                            compareDate: moment().diff(moment(result.date).format('LLL'), 'days')
                        });

                    }

                })
            }

            if (this.isAging) {
                this.GetValue();
            } else {
                this.total = this.paymentVoucherItem.reduce(function (a, c) {

                    return a + Number((c.isActive == true ? c.extraAmount < 0 ? c.extraAmount * -1 : c.extraAmount : 0) || 0)

                }, 0)

            }
        }
    }
</script>

<style scoped>
    .colorDiv {
        background-color: #efefef !important
    }
</style>
