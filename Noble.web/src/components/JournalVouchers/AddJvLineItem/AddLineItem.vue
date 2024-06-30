<template>
    <div>
        <div class=" mt-5" v-if="formName=='OpeningCash'">
            <table class="table itemtable add_table_list_bg ">
                <thead class="thead-light m-0">
                    <tr class="text-capitalize text-center">
                        <th class="text-center" style="width:30%;">   {{ $t('JvAddLineItem.Account') }}</th>
                        <th style="width:37%;">{{ $t('JvAddLineItem.description') }}</th>
                        <th style="width:15%;">{{ $t('JvAddLineItem.Debit') }}</th>
                        <th style="width:15%;">{{ $t('JvAddLineItem.Credit') }}</th>

                        <th style="width:3%;" class="text-center"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-left" v-for="(items,index) in journalVoucherItems" v-bind:key="items.id">
                        <td v-if="view">
                            <accountdropdown v-model="items.accountId" :disabled="true" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                        </td>
                        <td v-else>
                            <accountdropdown v-if="formName=='OpeningCash'" :PanelWidth="true" v-bind:fromName="OpeningCash" v-model="items.accountId" :disabled="false" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                            <accountdropdown v-else v-model="items.accountId" :disabled="false" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" :formName="formName" />
                        </td>
                        <td class="text-left">
                            <textarea class="form-control textarea_jv_line borderNone" v-if="view" disabled
                                      v-model="items.description" />
                            <textarea class="form-control textarea_jv_line borderNone" v-else
                                      v-model="items.description" />
                        </td>
                        <td>
                            <input v-model="items.debit" v-if="view" disabled @update:modelValue="zeroCredit(items.id)" class="form-control borderNone" :text-dir="'true'" />
                            <input v-model="items.debit" v-else @update:modelValue="zeroCredit(items.id)" class="form-control borderNone" :text-dir="'true'" />
                        </td>
                        <td>
                            <input v-model="items.credit" v-if="view" disabled @update:modelValue="zeroDebit(items.id)" class="form-control borderNone" :text-dir="'true'" />
                            <input v-model="items.credit" v-else @update:modelValue="zeroDebit(items.id)" class="form-control borderNone" :text-dir="'true'" />
                        </td>
                        <!--<td style="width:300px">
                    <contactempdropdown v-model="item.contactId" :value="item.contactId" :dropdownpo="'dropdownpo'" />
                </td>-->

                        <td v-if="index == journalVoucherItems.length - 1 && addItem == false">
                            <button title="Add New Item" disabled class="btn btn-primary btn-sm btn-round btn-icon float-right   " v-on:click="NewItem" v-if="view">
                                <i class="fa fa-check"></i>
                            </button>
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right  " v-on:click="NewItem" v-else>
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                        <td v-else class="text-center">
                            <button title="Remove Item" class="btn " disabled v-on:click="removeJvItem(items.id, 'true')" v-if="view">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button> <button title="Remove Item" v-else class="btn" v-on:click="removeJvItem(items.id, 'true')">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button>
                        </td>
                    </tr>

                    <tr v-if="addItem">
                        <td v-if="view">
                            <accountdropdown v-model="item.accountId" :disabled="true" :dropdownaccount="'dropdownAccountcss'" :value="item.accountId" :dropdownpo="'dropdownpo'" @update:modelValue="autoChange" :key="refresh" />
                        </td>
                        <td v-else>
                            <accountdropdown v-model="item.accountId" :PanelWidth="true" :dropdownaccount="'dropdownAccountcss'" :value="item.accountId" :dropdownpo="'dropdownpo'" @update:modelValue="autoChange" :formName="formName" :key="refresh" />
                        </td>

                        <td class="text-left">
                            <textarea class="form-control textarea_jv_line borderNone"
                                      v-model="item.description" />
                        </td>
                        <td class="text-center"> <input v-model="item.debit" type="number" @click="$event.target.select()" v-on:blur="autoChange" class="form-control borderNone text-center text-center" /></td>
                        <td class="text-center"> <input v-model="item.credit" type="number" @click="$event.target.select()" v-on:blur="autoChange" class="form-control borderNone text-center text-center" /></td>

                        <td>
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right" v-if="view" disabled v-on:click="NewItem">
                                <i class="fa fa-check"></i>
                            </button>
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right" v-else :disabled="v$.item.$invalid" v-on:click="NewItem">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>

            </table>
            <hr />
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-8 "></div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4 ">
                    <div class="mt-4">
                        <table class="table" style="background-color: #F1F5FA;">
                            <tbody>
                                <tr>
                                    <td colspan="2" width="40%"><h5 class="m-0 text-start">{{ $t('JvAddLineItem.Total') }} :</h5></td>
                                    <td>
                                        <h6 style="padding:13px" class="m-0 text-end">{{currency}} {{$roundOffFilter(totalDebitAmounts)  }}</h6>
                                    </td>
                                    <td>
                                        <h6 style="padding:13px" class="m-0 text-end">{{currency}} {{$roundOffFilter(totalCreditAmount) }}</h6>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" width="40%"><h5 class="m-0 text-start">{{ $t('JvAddLineItem.Balance') }}  :</h5></td>
                                    <td>
                                        <h6 style="padding:13px" class="m-0 text-end"> {{currency}} {{$roundOffFilter((totalDebitAmounts - totalCreditAmount))  }}</h6>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

        </div>


        <div v-else class="mt-5">
            <table class="table">
                <thead class="thead-light m-0">
                    <tr class="text-center">
                        <th class="text-center" style="width:25%;">   {{ $t('JvAddLineItem.Account') }}</th>
                        <th style="width:14%;" v-if="formName=='JournalVoucher'"> {{ $t('JvAddLineItem.PaymentMode') }}</th>
                        <th style="width:14%;" v-if="formName=='JournalVoucher'">  {{ $t('JvAddLineItem.PaymentType') }} </th>
                        <th style="width:10%;" v-if="formName=='JournalVoucher'">{{ $t('JvAddLineItem.ChequeNo') }} </th>
                        <th style="width:14%;">{{ $t('JvAddLineItem.description') }}</th>
                        <th style="width:10%;">{{ $t('JvAddLineItem.Debit') }}</th>
                        <th style="width:10%;">{{ $t('JvAddLineItem.Credit') }}</th>
                        <!--<th >Name</th>-->
                        <th style="width:3%;"></th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-left" v-for="(items,index) in journalVoucherItems" v-bind:key="items.id">
                        <td v-if="view">
                            <accountdropdown v-model="items.accountId" :disabled="true" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                        </td>
                        <td v-else>
                            <!--<accountdropdown  v-model="items.accountId" :disabled="false" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />-->
                            <accountdropdown v-if="formName=='OpeningCash'" v-bind:fromName="OpeningCash" v-model="items.accountId" :disabled="false" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                            <accountdropdown v-else v-model="items.accountId" :PanelWidth="true" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" :formName="formName" />
                        </td>
                        <td v-if="formName=='JournalVoucher'" v-bind:class="dropdownAccountcss">
                            <div class="dropdownAccountcss">
                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) && view " disabled v-model="items.paymentMode" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                                </multiselect>
                                <multiselect v-else-if="($i18n.locale == 'en' ||isLeftToRight()) && view==false " v-model="items.paymentMode" :options="['Cash', 'Bank']" :show-labels="false" placeholder="Select Type">
                                </multiselect>
                                <multiselect v-else-if="$i18n.locale == 'ar' && view" disabled v-model="items.paymentMode" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')">
                                </multiselect>
                                <multiselect v-else-if="$i18n.locale == 'ar' && view==false" v-model="items.paymentMode" :options="[ 'السيولة النقدية', 'مصرف']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')">
                                </multiselect>
                            </div>

                        </td>
                        <td v-if="formName=='JournalVoucher'" v-bind:class="dropdownAccountcss">
                            <div class="dropdownAccountcss">
                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) && view " disabled v-model="items.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type" style="border:none !important">
                                </multiselect>
                                <multiselect v-else-if="($i18n.locale == 'en' ||isLeftToRight()) && view==false && items.paymentMode=='Cash' " disabled v-model="items.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type" style="border:none !important">
                                </multiselect>
                                <multiselect v-else-if="($i18n.locale == 'en' ||isLeftToRight()) && view==false " v-model="items.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type" style="border:none !important">
                                </multiselect>

                                <multiselect v-else-if="$i18n.locale == 'ar' && view" disabled v-model="items.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')" style="border:none !important">
                                </multiselect>
                                <multiselect v-else-if="$i18n.locale == 'ar' && view==false  && items.paymentMode=='السيولة النقدية'" disabled v-model="items.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')" style="border:none !important">
                                </multiselect>
                                <multiselect v-else-if="$i18n.locale == 'ar' && view==false" disabled v-model="items.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')" style="border:none !important">
                                </multiselect>
                            </div>

                        </td>
                        <td v-if="formName=='JournalVoucher'">
                            <input v-if="view" class="form-control borderNone" disabled
                                   v-model="items.chequeNo" />
                            <input v-else-if="items.paymentMethod== 'Cheque' || items.paymentMethod== 'التحقق من' " class="form-control borderNone"
                                   v-model="items.chequeNo" />
                            <input v-else disabled class="form-control borderNone"
                                   v-model="items.chequeNo" />
                        </td>
                        <td class="text-left">
                            <textarea class="form-control borderNone textarea_jv_line" v-if="view" disabled
                                      v-model="items.description" ></textarea>
                            <textarea class="form-control borderNone textarea_jv_line" v-else
                                      v-model="items.description"></textarea>
                        </td>
                        <td>
                            <input v-model="items.debit" v-if="view" disabled @update:modelValue="zeroCredit(items.id)" class="form-control text-center borderNone" :text-dir="'true'" />
                            <input v-model="items.debit" v-else @update:modelValue="zeroCredit(items.id)" class="form-control text-center  borderNone" :text-dir="'true'" />
                        </td>
                        <td>
                            <input v-model="items.credit" v-if="view" disabled @update:modelValue="zeroDebit(items.id)" class="form-control text-center borderNone" :text-dir="'true'" />
                            <input v-model="items.credit" v-else @update:modelValue="zeroDebit(items.id)" class="form-control text-center borderNone" :text-dir="'true'" />
                        </td>
                        <!--<td style="width:300px">
                    <contactempdropdown v-model="item.contactId" :value="item.contactId" :dropdownpo="'dropdownpo'" />
                </td>-->

                        <td style="width:50px" v-if="index == journalVoucherItems.length - 1 && addItem == false">
                            <button title="Add New Item" disabled class="btn btn-primary btn-sm btn-round btn-icon float-right" v-on:click="NewItem" v-if="view">
                                <i class="fa fa-check"></i>
                            </button>
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right" v-on:click="NewItem" v-else>
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                        <td style="width:50px" v-else>
                            <button title="Remove Item" class="btn float-right" disabled v-on:click="removeJvItem(items.id, 'true')" v-if="view">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button> <button title="Remove Item" v-else class="btn float-right" v-on:click="removeJvItem(items.id, 'true')">
                                <i class="las la-trash-alt text-secondary font-16"></i>
                            </button>
                        </td>
                    </tr>

                    <tr v-if="addItem">
                        <td v-if="view">
                            <accountdropdown v-model="item.accountId" :disabled="true" :dropdownaccount="'dropdownAccountcss'" :value="item.accountId" :dropdownpo="'dropdownpo'" @update:modelValue="autoChange" :key="refresh" />
                        </td>
                        <td v-else>
                            <accountdropdown v-model="item.accountId" :PanelWidth="true" :dropdownaccount="'dropdownAccountcss'" :value="item.accountId" :dropdownpo="'dropdownpo'" @update:modelValue="autoChange" :formName="formName" :key="refresh" />
                        </td>

                        <td v-if="formName=='JournalVoucher'">
                            <div class="dropdownAccountcss">
                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight()) " :options="['Cash', 'Bank']" v-model="item.paymentMode" :show-labels="false" placeholder="Select Type">
                                </multiselect>
                                <multiselect v-else :options="[ 'السيولة النقدية', 'مصرف']" v-model="item.paymentMode" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')">
                                </multiselect>
                            </div>

                        </td>
                        <td v-if="formName=='JournalVoucher'" v-bind:class="dropdownAccountcss">
                            <div class="dropdownAccountcss">
                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())  && item.paymentMode=='Cash' " disabled v-model="item.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type" style="border:none !important">
                                </multiselect>
                                <multiselect v-else-if="$i18n.locale == 'ar'   && item.paymentMode=='السيولة النقدية'" disabled v-model="item.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')" style="border:none !important">
                                </multiselect>
                                <multiselect v-else-if="($i18n.locale == 'en' ||isLeftToRight()) " v-model="item.paymentMethod" :options="['Cheque', 'Transfer','Deposit']" :show-labels="false" placeholder="Select Type">
                                </multiselect>

                                <multiselect v-else v-model="item.paymentMethod" :options="[ 'التحقق من', 'تحويل','الوديعة']" :show-labels="false" v-bind:placeholder="$t('JvAddLineItem.SelectOption')">
                                </multiselect>
                            </div>

                        </td>
                        <td v-if="formName=='JournalVoucher'">
                            <input v-if="item.paymentMethod== 'Cheque' || item.paymentMethod== 'التحقق من' " class="form-control borderNone"
                                   v-model="item.chequeNo" />
                            <input v-else class="form-control input-border" disabled
                                   v-model="item.chequeNo" />
                        </td>
                        <td class="text-left">
                            <textarea class="form-control borderNone textarea_jv_line"
                                      v-model="item.description" />
                        </td>
                        <td class="text-center"> <input v-model="item.debit" type="number" @click="$event.target.select()" v-on:blur="autoChange" class="form-control borderNone text-center text-center" /></td>
                        <td class="text-center"> <input v-model="item.credit" type="number" @click="$event.target.select()" v-on:blur="autoChange" class="form-control borderNone text-center text-center" /></td>
                        <!--<td style="width:120px"> <input v-model="item.debit" :value="item.debit" v-on:change.native="autoChange" class="itemdata" :text-dir="'true'" :key="refresh" /></td>
                <td style="width:120px"> <input v-model="item.credit" :value="item.credit" v-on:change.native="autoChange" class="itemdata" :text-dir="'true'" :key="refresh" /></td>-->
                        <td style="width:50px;">
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right" v-if="view" disabled v-on:click="NewItem">
                                <i class="fa fa-check"></i>
                            </button>
                            <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " v-else :disabled="v$.item.$invalid" v-on:click="NewItem">
                                <i class="fa fa-check"></i>
                            </button>
                        </td>
                    </tr>
                </tbody>

            </table>
            <hr />
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-8 "></div>
                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4 ">
                    <div class="mt-4">
                        <table class="table" style="background-color: #F1F5FA;">
                            <tbody>
                                <tr>
                                    <td colspan="2" width="40%"><h5 class="m-0 text-start">{{ $t('JvAddLineItem.Total') }} :</h5></td>
                                    <td>
                                        <h6 style="padding:13px" class="m-0 text-end">{{currency}} {{$roundOffFilter(totalDebitAmounts)  }}</h6>
                                    </td>
                                    <td>
                                        <h6 style="padding:13px" class="m-0 text-end">{{currency}} {{$roundOffFilter(totalCreditAmount)  }}</h6>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" width="40%"><h5 class="m-0 text-start">{{ $t('JvAddLineItem.Balance') }}  :</h5></td>
                                    <td>
                                        <h6 style="padding:13px" class="m-0 text-end"> {{currency}} {{$roundOffFilter((totalDebitAmounts - totalCreditAmount))  }}</h6>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        </div>
</template>
<script>
    import { required, numeric, sameAs, not } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Multiselect from 'vue-multiselect'


    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        props: ["journalVoucherItem", "value", "view", "formName"],
        components: {
            Multiselect,

        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                journalVoucherItems: [],
                currency: '',
                item: {
                    id: '',
                    accountId: '',
                    description: '',
                    paymentMode: '',
                    paymentMethod: '',
                    debit: 0.00,
                    credit: 0.00,
                    contactId: '',
                    chequeNo: '',
                },
                loading: false,
                refresh: 0,
                currentItem: {
                    id: '',
                    type: '',
                    amount: 0
                },
                addItem: false,
                tableLength: 0,
                cardLength: 0,
                dropdownAccountcss: 'dropdownAccountcss',
            }
        },
        validations() {
            return {
                item: {
                    debit: {
                        required: required,
                        numeric: numeric,
                        zeroCredit: function (value) {
                            if (value > 0) {
                                this.item.credit = 0;
                            }
                            return true;
                        },
                        sameAsCreditAmount: not(sameAs('credit'))

                    },
                    credit: {
                        required: required,
                        numeric: numeric,
                        zeroDebit: function (value) {
                            if (value > 0) {
                                this.item.debit = 0;
                            }
                            return true;
                        },
                        sameAsDebitAmount: not(sameAs('debit'))
                    },
                    accountId: {
                        required: required
                    }
                }
                }
        },
        filters: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },

        computed: {
            totalDebitAmounts: function () {
                var total = 0;

                if (this.journalVoucherItems !== null && this.journalVoucherItems.length > 0) {
                    this.journalVoucherItems.forEach(function (item) {

                        total = total + parseFloat(item.debit);

                    })
                }

                this.$emit('totalDebitAmounts', total);
                return total;
            },
            totalCreditAmount: function () {
                var total = 0;

                if (this.journalVoucherItems !== null && this.journalVoucherItems.length > 0) {
                    this.journalVoucherItems.forEach(function (item) {

                        total = total + parseFloat(item.credit);

                    })
                }
                this.$emit('totalCreditAmount', total);

                return total;
            },
        },

        methods: {
            autoChange: function () {
           /*     this.checkTableWidth();*/

                if (this.item.accountId != '' && this.item.taxCodeId != '' && this.item.debit != this.item.credit) {
                    this.addJvItem();
                }
            },

            zeroCredit: function (id) {

               /* this.checkTableWidth();*/

                var item = this.journalVoucherItems.find(function (x) { return x.id == id });
                //for auto add jvItem
                if (this.currentItem.id == id) {
                    this.currentItem.type = "Debit";
                    this.currentItem.amount = item.debit;
                }
                item.credit = 0;
            },

            //checkTableWidth: function () {
            //    if (document.getElementsByClassName('itemtable')[0] != undefined) {
            //        this.tableLength = document.getElementsByClassName('itemtable')[0].clientWidth;
            //        this.cardLength = document.getElementsByClassName('card')[0].clientWidth - 25;
            //    }
            //},

            zeroDebit: function (id) {

              /*  this.checkTableWidth();*/
                var item = this.journalVoucherItems.find(function (x) { return x.id == id });
                //for auto add jvItem
                if (this.currentItem.id == id) {
                    this.currentItem.type = "Credit";
                    this.currentItem.amount = item.credit;
                }
                item.debit = 0;
            },

            addJvItem: function (value) {
                this.loading = true;
                this.$emit('itemLoading', this.loading);

                if (this.item.accountId != '' && this.item.taxCodeId != '') {
                    this.item.id = this.createUUID();

                    this.journalVoucherItems.push({
                        id: this.item.id,
                        accountId: this.item.accountId,
                        description: this.item.description,
                        paymentMode: this.item.paymentMode,
                        paymentMethod: this.item.paymentMethod,
                        debit: this.item.debit,
                        credit: this.item.credit,
                        chequeNo: this.item.chequeNo,
                        contactId: this.item.contactId
                    });
                    this.addItem = false;
                }
                this.loading = false;
                this.$emit('itemLoading', this.loading);
                this.refresh += 1;
                if (value != 'AutoCall') {
                    if (this.item.debit > 0) {
                        this.currentItem.id = this.item.id;
                        this.currentItem.type = 'Debit';
                        this.currentItem.amount = this.item.debit;
                    } else {
                        this.currentItem.id = this.item.id;
                        this.currentItem.type = 'Credit';
                        this.currentItem.amount = this.item.credit;
                    }
                }
            },

            NewItem: function () {
                
                this.addItem = true;
               /* this.checkTableWidth();*/

                this.item = {
                    accountId: '',
                    description: '',
                    paymentMode: '',
                    paymentMethod: '',
                    debit: 0.00,
                    credit: 0.00,
                    contactId: '',
                    chequeNo: '',

                }
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

            removeJvItem: function (id) {

                var ds = this.journalVoucherItems.findIndex(function (i) {
                    return i.id === id;
                });

                this.journalVoucherItems.splice(ds, 1);
            },


            //updateData: function (id, oldId) {
            //    var root = this;
            //    var token = '';
            //    if (token == '') {
            //        token = localStorage.getItem('token');
            //    }
            //    const config = {
            //        headers: { Pragma: 'no-cache' },
            //        params: { id: id }
            //    }
            //    this.loading = true;
            //    var data = root.journalVoucherItems.find(x => x.id == oldId);
            //    data.isUpdated = true;
            //    this.$emit('itemLoading', this.loading);
            //    root.$https.get('/Item/GetItem', config, { headers: { "Authorization": `Bearer ${token}` } })
            //        .then(function (response) {
            //            if (response.data.value) {
            //                var codeId = "";
            //                if (response.data.item.taxCode != null) {
            //                    codeId = response.data.item.taxCode.code;
            //                } else {
            //                    codeId = response.data.item.taxCodeId
            //                }

            //                if (response.data.item.code != null) {
            //                    data.code = response.data.item.code;
            //                    data.accountCode = "";
            //                    data.accountId = response.data.item.accountId;
            //                    data.name = response.data.item.name;
            //                    data.description = response.data.item.description;
            //                    data.unitCost = response.data.item.unitCost;
            //                    data.discount = response.data.item.discount;
            //                    data.taxCodeId = codeId;
            //                    data.journalVoucherId = root.value.id;
            //                    data.quantity = 1;
            //                }
            //                root.showModal = !root.showModal;
            //                data.isUpdated = false;
            //            } else {
            //                console.log("error: something wrong from db.");
            //            }
            //            root.loading = false;
            //            root.dataLoading = false;

            //            root.$emit('itemLoading', root.loading);
            //        },
            //            function (error) {
            //                this.loading = false;
            //                console.log(error);
            //            });
            //}

        },
        updated: function () {
            document.querySelector("html").classList.remove("perfect-scrollbar-on");
            this.$emit('updatejournalVoucherItems', this.journalVoucherItems);
         /*   this.checkTableWidth();*/
        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
            this.journalVoucherItems = this.journalVoucherItem;

            this.addItem = this.journalVoucherItems.length > 0 ? false : true;

        }
    }</script>
<style scoped>
    .input-border {
       
       
    }

        .input-border:focus {
          
        }

    .borderNone {
      
    }

    .textarea_jv_line {
    /*    max-height: 40px !important;
        height: 40px !important;
        padding: 0.375rem 0.75rem;*/
    }

</style>