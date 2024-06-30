<template>
    <div>
        <div class="" v-bind:key="rendered">
            <table class="table mb-0" style="table-layout:fixed;">
                <thead class="thead-light">
                    <tr>
                        <th style="width: 6%;">
                            #
                        </th>
                        <th style="width:22%;">
                            {{ $t('CreditPaymentMode.PaymentMode') }}
                        </th>
                        <th style="width:22%;" class="text-center">
                            {{ $t('CreditPaymentMode.BankAccount') }}
                        </th>
                        <th style="width:22%;" class="text-center">
                            {{ $t('CreditPaymentMode.Description') }}
                        </th>
                        <th class="text-center" style="width: 22%;">
                            {{ $t('CreditPaymentMode.Amount') }}
                        </th>

                        <th style="width:6%;"></th>
                    </tr>
                </thead>
                <tbody :key="rendered">

                    <tr v-for="(pay, index) in creditPayment" :key="index" style="background: rgb(234, 241, 254);">
                        <td>{{index+1}}</td>
                        <td>
                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="pay.paymentMode" :options="['Cash', 'Bank']" :show-labels="false" :placeholder="$t('CreditPaymentMode.PaymentMode')">
                            </multiselect>
                            <multiselect v-else disabled v-model="pay.paymentMode" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" :placeholder="$t('CreditPaymentMode.PaymentMode') ">
                            </multiselect>
                        </td>
                        <td>
                            <accountdropdown v-model="pay.accountId" :formName="BankPay" :disabled="(pay.paymentMode == 'Bank' ||  pay.paymentMode == 'مصرف')?false:true" :isPurchase="false"></accountdropdown>
                        </td>
                        <td>
                            <input class="form-control borderNone"
                                   v-model="pay.description" style="background-color: #ffffff;" />

                        </td>
                        <td>
                            <decimal-to-fixed v-model="pay.amount"
                                              v-bind:salePriceCheck="false" />
                        </td>
                        <td>
                            <a href="javascript:void(0);" v-on:click="removeItem(pay.id)"><i class="las la-trash-alt text-secondary font-16"></i></a>

                            <!--<button title="Remove Item" v-on:click="removeItem(pay.id)">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button>-->
                        </td>

                    </tr>

                </tbody>
                <tbody>
                    <template v-if="addNewItem">
                        <tr style="background: rgb(234, 241, 254);">
                            <td></td>
                            <td>
                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="newItem.paymentMode" :options="['Cash', 'Bank']" :show-labels="false" :placeholder="$t('CreditPaymentMode.PaymentMode')">
                                </multiselect>
                                <multiselect v-else disabled v-model="newItem.paymentMode" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" :placeholder="$t('CreditPaymentMode.PaymentMode')">
                                </multiselect>
                            </td>
                            <td>
                                <accountdropdown v-model="newItem.accountId" :disabled="(newItem.paymentMode == 'Bank' ||  newItem.paymentMode == 'مصرف')?false:true" :formName="'BankReceipt'" :isPurchase="false"></accountdropdown>
                            </td>
                            <td>
                                <input class="form-control borderNone"
                                       v-model="newItem.description" style="background-color: #ffffff;" />

                            </td>
                            <td>
                                <decimal-to-fixed v-model="newItem.amount"
                                                  v-bind:salePriceCheck="false" />


                            </td>
                            <td>
                                <a href="javascript:void(0);" :disabled="v$.newItem.$invalid" v-on:click="AddNewItem"> <i class="fa fa-check"></i></a>

                                <!--<button title="Add New Item" class="btn btn-danger btn-round btn-sm btn-icon " :disabled="v$.newItem.$invalid" v-on:click="AddNewItem">
                                    <i class="nc-icon bord rounded-circle nc-simple-add"></i>
                                </button>-->
                            </td>

                        </tr>
                    </template>
                </tbody>
                <tbody>
                    <tr style="background: rgb(234, 241, 254);">
                        <td colspan="4" style="text-align:right;font-size:16px;font-weight:bold;">{{ $t('CreditPaymentMode.Total') }}  </td>
                        <td colspan="2" style="text-align: center; font-size: 16px; font-weight: bold;">{{$roundOffFilter(totalAmount)  }}</td>

                    </tr>
                </tbody>
            </table>
        </div>

    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    import { required, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    export default {
        name: "CreditPayment",
        mixins: [clickMixin],
        props: ['creditPaymentData', 'isDuplicate'],
        components: {
            Multiselect
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                creditPayment: [],
                newItem: {
                    paymentMode: '',
                    accountId: '',
                    description: '',
                    amount: '0'
                },
                addNewItem: false,
                rendered: 0
            };
        },

        validations() {
            return {
                newItem: {
                    paymentMode: {
                        required: required
                    },
                    amount: {
                        required: required,
                    },
                    accountId: {
                        required: requiredIf((x) => {
                            if (x.paymentMode == 'Bank' || x.paymentMode == 'مصرف')
                                return true;
                            return false;
                        }),
                    }
                }
            }
        },
        filter: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },

        computed: {
            totalAmount: function () {
                var total = 0;

                if (this.creditPayment != null && this.creditPayment.length > 0) {
                    this.creditPayment.forEach(function (item) {
                        item.amount = (item.amount == '' || item.amount == null) ? 0 : item.amount
                        total = total + parseFloat(item.amount);

                    })
                }

                this.$emit('update:modelValue', this.creditPayment);
                return total;
            },

        },
        methods: {
            AddNewItem: function () {
                this.creditPayment.push({
                    id: this.createUUID(),
                    paymentMode: this.newItem.paymentMode,
                    accountId: this.newItem.accountId,
                    description: this.newItem.description,
                    amount: parseFloat(this.newItem.amount)
                })

                this.newItem.paymentMode = '';
                this.newItem.accountId = '';
                this.newItem.description = '';
                this.newItem.amount = '';
                this.rendered++
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
            removeItem: function (id) {

                var ds = this.creditPayment.findIndex(function (i) {
                    return i.id === id;
                });

                this.creditPayment.splice(ds, 1);
            },

        },
        created: function () {
            this.addNewItem = true;
            var root = this;
            if (this.creditPaymentData !== null && this.creditPaymentData.length > 0) {
                this.creditPaymentData.forEach(function (item) {

                    if (item.name != 'Credit' && root.isDuplicate == false) {
                        root.creditPayment.push({
                            id: item.id,
                            paymentMode: item.name,
                            accountId: item.bankCashAccountId,
                            description: item.description,
                            amount: item.amount
                        })
                    }

                })
            }
        },
        mounted: function () {

        },
    };
</script>
<style scoped>

    #sale-item td {
        padding-bottom: 5px;
        padding-top: 5px;
    }

    .input-border {
        border: transparent;
        background-color: transparent !important;
    }

        .input-border:focus {
            outline: none !important;
            border: none !important;
        }

    .multiselect__tags {
        background-color: transparent !important;
    }

    .tableHoverOn {
        background-color: #ffffff !important;
        height: 32px !important;
        max-height: 32px !important;
    }

    .multiselect__input, .multiselect__single {
        background-color: transparent !important;
    }

    .samary_tbl_pd td {
        padding: 20px 7px !important;
    }
</style>