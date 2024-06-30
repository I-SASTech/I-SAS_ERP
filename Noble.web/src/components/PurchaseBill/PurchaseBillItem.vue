<template>
    <div>
        <div v-bind:class="{ 'horizontal-table': tableLength >= cardLength }" v-if="isDisable == 'true'">
            <div class=" mt-4">
                <table class="table mb-0">
                    <thead class="thead-light table-hover">
                        <tr class="text-capitalize text-center">
                            <th style="width: 5%;">
                                #
                            </th>
                            <th class="text-start" style="width:50%;"> {{ $t('PurchaseBillItem.Description') }}</th>
                            <th style="width:25%;">{{ $t('PurchaseBillItem.Account') }}</th>
                            <th class="text-end" style="width:20%;">{{ $t('PurchaseBillItem.Amount') }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(expense, index) in purchaseBillItems" v-bind:key="expense.id">
                            <td>{{ index + 1 }}</td>
                            <td>
                                <textarea rows="2" class="form-control input-border" v-model="expense.description" disabled
                                    v-bind:placeholder="$t('WriteHere')" />
                            </td>
                            <td>
                                <accountdropdown v-model="expense.accountId" :disabled="true"
                                    :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                            </td>
                            <td>
                                <decimal-to-fixed v-bind:salePriceCheck="false" :disabled="true" v-model="expense.amount"
                                    :text-dir="'true'" />

                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <h6 class="m-0 text-left"> &nbsp;</h6>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 "></div>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div>
                            <table class="table " style="background-color: #F1F5FA;">
                                <tbody>
                                    <tr>
                                        <td colspan="2" class="pt-3 fw-bold " style="width: 65%;">
                                            {{ $t('PurchaseBillItem.TotalAmount') }}
                                        </td>
                                        <td class="text-end" style="width: 35%;">
                                            {{ currency }} {{$roundOffFilter(totalAmount)   }}
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-bind:class="{ 'horizontal-table': tableLength >= cardLength }" v-else>
            <div class="  mt-4 ">
                <table class="table">
                    <thead class="thead-light table-hover">
                        <tr class="text-capitalize text-center">
                            <th style="width: 5%;">
                                #
                            </th>
                            <th style="width:45%;" class="text-start">{{ $t('PurchaseBillItem.Description') }}</th>
                            <th style="width:25%;">{{ $t('PurchaseBillItem.Account') }}</th>
                            <th style="width:25%;">{{ $t('PurchaseBillItem.Amount') }}</th>
                            <th style="width:5%;" class="text-center">{{ $t('PurchaseBillItem.Action') }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(expense, index) in purchaseBillItems" v-bind:key="expense.id">
                            <td>{{ index + 1 }}</td>
                            <td>
                                <textarea rows="2" class="form-control" v-model="expense.description"
                                    v-bind:placeholder="$t('PurchaseBillItem.WriteHere')" />
                            </td>
                            <td>
                                <accountdropdown v-model="expense.accountId" :PanelWidth="true" :disabled="false"
                                    :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                            </td>
                            <td class="text-center">
                                <decimal-to-fixed v-bind:salePriceCheck="false" v-model="expense.amount"
                                    :text-dir="'true'" />
                            </td>
                            <td class="text-center" v-if="index == purchaseBillItems.length - 1 && addItem == false">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right  "
                                    v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else>
                                <button title="Remove Item" class="btn r  btn-sm   "
                                    v-on:click="removeExpense(expense.id, 'true')">
                                    <i class="las la-trash-alt text-danger font-16"></i>
                                </button>
                            </td>
                        </tr>
                        <tr v-if="addItem">
                            <td></td>
                            <td>
                                <textarea rows="2" class="form-control input-border" v-model="purchaseBills.description"
                                    v-bind:placeholder="$t('PurchaseBillItem.WriteHere')" style="width:100% !important;" />
                            </td>
                            <td>
                                <accountdropdown v-model="purchaseBills.accountId" :PanelWidth="true" :disabled="false"
                                    :dropdownaccount="'dropdownAccountcss'" :value="purchaseBills.accountId"
                                    :dropdownpo="'dropdownpo'" :key="refresh" />
                            </td>
                            <td>
                                <decimal-to-fixed v-bind:salePriceCheck="false" v-model="purchaseBills.amount" />
                            </td>
                            <td class="text-center" v-if="(purchaseBills.description == '')">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right"
                                    disabled v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="(purchaseBills.accountId == '')">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right "
                                    disabled v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="(purchaseBills.amount == 0)">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right "
                                    disabled v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else>
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right"
                                    v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                        </tr>
                        <tr style="border:none !important;">
                            <td colspan="4">
                                <h6 class="m-0 text-left"> &nbsp;</h6>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6"></div>
                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                        <div>
                            <table class="table " style="background-color: #F1F5FA;">
                                <tbody>
                                    <tr>
                                        <td colspan="2" class="pt-3 fw-bold " style="width: 65%;">
                                            {{ $t('PurchaseBillItem.TotalAmount') }}
                                        </td>
                                        <td class="text-end" style="width: 35%;">
                                            {{ currency }} {{$roundOffFilter(totalAmount)   }}
                                        </td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>

<script>

import clickMixin from '@/Mixins/clickMixin'
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default {
    mixins: [clickMixin],
    props: ['isDisable'],
    components: {
            Loading
        },
    data: function () {
        return {
            purchaseBillItems: [],
            purchaseBills: {
                id: '',
                description: '',
                amount: 0.00,
                accountId: ''
            },
            loading: false,
            refresh: 0,
            currentItem: {
                id: '',
                description: '',
                amount: 0.00,
                accountId: ''
            },
            addItem: false,
            IsExpenseAccount: false,
            tableLength: 0,
            cardLength: 0,
            currency: '',
        }
    },
    filters: {
        roundOffFilter: function (value) {

            return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        }
    },
    computed: {
        totalAmount: function () {
            var total = 0;
            if (this.purchaseBillItems != undefined) {
                if (this.purchaseBillItems !== null && this.purchaseBillItems.length > 0) {
                    this.purchaseBillItems.forEach(function (purchaseBills) {

                        total = parseFloat(total) + parseFloat(purchaseBills.amount);

                    })
                }

                return total;
            }
            return 0;
        },
    },

    methods: {
        checkTableWidth: function () {
            if (document.getElementsByClassName('itemtable')[0] != undefined) {
                this.tableLength = document.getElementsByClassName('itemtable')[0].clientWidth;
                this.cardLength = document.getElementsByClassName('card')[0].clientWidth - 25;
            }
        },
        addDailyExpense: function () {

            this.loading = true;
            this.purchaseBillItems.push({
                id: this.createUUID(),
                amount: this.purchaseBills.amount,
                accountId: this.purchaseBills.accountId,
                description: this.purchaseBills.description
            });

            this.purchaseBills = {
                id: '',
                amount: 0.00,
                description: '',
                accountId: '',
            };
            this.refresh += 1;
            this.loading = false;
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

            var ds = this.purchaseBillItems.findIndex(function (i) {
                return i.id === id;
            });

            this.purchaseBillItems.splice(ds, 1);
        },
    },
    created: function () {
        if (this.$route.query.data != undefined) {
            this.purchaseBillItems = this.$route.query.data.purchaseBillItems;
            this.addItem = true;
            this.refresh += 1;
        }
        else
        {
            this.addItem = this.purchaseBillItems.length > 0 ? false : true;
        }
    },
    updated: function () {
        document.querySelector("html").classList.remove("perfect-scrollbar-on");
        this.$emit('updatedailyExpenseRows', this.purchaseBillItems);
    },
    mounted: function () {
        this.currency = localStorage.getItem('currency');
        this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
        // this.addItem = this.purchaseBillItems.length > 0 ? false : true;
    }
}</script>