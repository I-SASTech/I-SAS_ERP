<template>
    <div class="row"
        v-if=" ((isValid('CanDraftJV') || isValid('CanEditJV') || isValid('CanAddJV')) && formName == 'JournalVoucher')   ||    ((isValid('CanAddOC') || isValid('CanEditOC') || isValid('CanDraftOC')) && formName == 'OpeningCash')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col" v-if="view">
                                <h4 v-if="journalVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'JournalVoucher'"
                                    class="page-title">{{ $t('AddJournalVoucher.JournalVouchers') }}</h4>
                                <h4 v-if="journalVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'OpeningCash'"
                                    class="page-title"> {{ $t('AddJournalVoucher.OpeningCashList') }}</h4>

                            </div>
                            <div class="col" v-else>
                                <h4 v-if="journalVoucher.id === '00000000-0000-0000-0000-000000000000' && formName == 'JournalVoucher'"
                                    class="page-title"> {{ $t('AddJournalVoucher.AddJournalVoucher') }}</h4>
                                <h4 v-if="journalVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'JournalVoucher'"
                                    class="page-title">{{ $t('AddJournalVoucher.UpdateJournalVoucher') }}</h4>
                                <h4 v-if="journalVoucher.id === '00000000-0000-0000-0000-000000000000' && formName == 'OpeningCash'"
                                    class="page-title"> {{ $t('AddJournalVoucher.OpeningCash') }}</h4>
                                <h4 v-if="journalVoucher.id != '00000000-0000-0000-0000-000000000000' && formName == 'OpeningCash'"
                                    class="page-title"> {{ $t('AddJournalVoucher.OpeningCash') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Sale.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="row form-group">
                                <label class="col-form-label col-lg-4">
                                    <label v-if="formName == 'OpeningCash'">{{ $t('AddJournalVoucher.DocumentNumber')
                                    }}:</label>
                                    <label v-if="formName == 'JournalVoucher'">{{ $t('AddJournalVoucher.JVNumber') }}:</label>
                                </label>
                                <div class="inline-fields col-lg-8"
                                    v-bind:class="{ 'has-danger': v$.journalVoucher.voucherNumber.$error }">
                                    <input disabled v-bind:key="renderVNo" v-model="v$.journalVoucher.voucherNumber.$model"
                                        class="form-control">
                                    <span v-if="v$.journalVoucher.voucherNumber.$error" class="error text-danger">
                                        <span v-if="!v$.journalVoucher.voucherNumber.required">{{
                                            $t('AddJournalVoucher.VoucherNumberRequired') }}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label class="col-form-label col-lg-4">
                                    <label v-if="formName == 'OpeningCash'">{{ $t('AddJournalVoucher.Date') }}:</label>
                                    <label v-if="formName == 'JournalVoucher'">{{ $t('AddJournalVoucher.JvDate') }}:</label>
                                </label>
                                <div class="inline-fields col-lg-8"
                                    v-bind:class="{ 'has-danger': v$.journalVoucher.voucherNumber.$error }">
                                    <div v-if="view">
                                        <datepicker v-bind:key="rander" :isDisable="true"
                                            v-model="v$.journalVoucher.date.$model" v-on:date="updateDate"></datepicker>
                                    </div>
                                    <div v-else>
                                        <datepicker v-bind:key="rander" v-model="v$.journalVoucher.date.$model"
                                            v-on:date="updateDate"></datepicker>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="row form-group">
                                <label class="col-form-label col-lg-4">
                                    <label> {{ $t('AddJournalVoucher.Narration') }}:</label>
                                </label>
                                <div class="inline-fields col-lg-8">
                                    <div v-if="view">
                                        <textarea rows="3" v-model="journalVoucher.narration" disabled
                                            class="form-control" />
                                    </div>
                                    <div v-else>
                                        <textarea rows="3" v-model="journalVoucher.narration" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <jvitems id="journalVoucherItems" :journalVoucherItem="journalVoucherItems" v-model="journalVoucher"
                        :key="rander" :view="view" :formName="formName"
                        v-on:updatejournalVoucherItems="updatejournalVoucherItems" v-on:itemLoading="itemLoading"
                        v-on:totalCreditAmount="totalCredit" v-on:totalDebitAmounts="totalDebit"></jvitems>
                </div>
                <div class="col-lg-12 mt-4 mb-5">
                    <div class="card">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8" style="border-right: 1px solid rgb(238, 238, 238);" v-if="view">
                                    <div class="form-group pe-3">
                                        <label> {{ $t('AddJournalVoucher.Comments') }}</label>
                                        <textarea v-model="journalVoucher.comments" disabled maxlength="500" rows="3"
                                            class="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-8" style="border-right: 1px solid rgb(238, 238, 238);" v-else>
                                    <div class="form-group pe-3">
                                        <label> {{ $t('AddJournalVoucher.Comments') }}</label>
                                        <textarea v-model="journalVoucher.comments" maxlength="500" class="form-control"
                                            rows="3" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ps-3">
                                        <div class="font-xs mb-1">{{ $t('AddJournalVoucher.AttachFile') }}</div>
                                        <button type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"
                                            v-on:click="Attachment()">
                                            <i class="fas fa-cloud-upload-alt"></i> {{ $t('AddJournalVoucher.Attachment') }}
                                        </button>
                                        <div>
                                            <small class="text-muted">{{ $t('AddJournalVoucher.FileSize') }} </small>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- <loading v-model:active="loading" :can-cancel="false" :is-full-page="false"></loading> -->
                <div class="col-lg-12 invoice-btn-fixed-bottom">
                    <div>
                        <div class="button-items" v-if="journalVoucher.id === '00000000-0000-0000-0000-000000000000'">
                            <button
                                v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) && (journalVoucherItems.length > 0) ? false : true"
                                type="button" v-on:click="OnSubmit('Draft')"
                                v-if="isValid('CanDraftJV') && formName == 'JournalVoucher'"
                                class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                    $t('AddJournalVoucher.SaveAsDraft') }}</button>
                            <button
                                v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) && (journalVoucherItems.length > 0) ? false : true"
                                type="button" v-on:click="OnSubmit('Draft')"
                                v-if="isValid('CanDraftOC') && formName == 'OpeningCash'"
                                class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                    $t('AddJournalVoucher.SaveAsDraft') }}</button>
                            <button
                                v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) && (journalVoucherItems.length > 0) ? false : true"
                                type="button" v-on:click="OnSubmit('Approved')"
                                v-if="(isValid('CanAddJV') || isValid('CanEditJV')) && formName == 'JournalVoucher'"
                                class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                    $t('AddJournalVoucher.SaveAsPost') }}</button>
                            <button
                                v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) && (journalVoucherItems.length > 0) ? false : true"
                                type="button" v-on:click="OnSubmit('Approved')"
                                v-if="(isValid('CanAddOC') || isValid('CanEditOC')) && formName == 'OpeningCash'"
                                class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                    $t('AddJournalVoucher.SaveAsPost') }}</button>

                            <button v-on:click="goToList" class="btn btn-danger "> {{ $t('AddJournalVoucher.Cancel')
                            }}</button>
                        </div>
                        <div v-else>
                            <div v-if="view">
                                <button v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) ? false : true" type="button"
                                    v-on:click="OnSubmit('Approved')" class="btn btn-outline-primary mx-2  "><i
                                        class="far fa-save"></i> Update Attachment</button>
                                <button v-on:click="goToList" class="btn btn-danger "> {{ $t('AddJournalVoucher.Cancel')
                                }}</button>
                            </div>
                            <div v-else class="button-items">
                                <button v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) ? false : true" type="button"
                                    v-on:click="OnSubmit('Draft')"
                                    v-if="isValid('CanDraftJV') && formName == 'JournalVoucher'"
                                    class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                        $t('AddJournalVoucher.UpdateAsDraft') }}</button>
                                <button v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) ? false : true" type="button"
                                    v-on:click="OnSubmit('Draft')" v-if="isValid('CanDraftOC') && formName == 'OpeningCash'"
                                    class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                        $t('AddJournalVoucher.UpdateAsDraft') }}</button>
                                <button v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) ? false : true" type="button"
                                    v-on:click="OnSubmit('Approved')"
                                    v-if="(isValid('CanAddJV') || isValid('CanEditJV')) && formName == 'JournalVoucher'"
                                    class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                        $t('AddJournalVoucher.UpdateAsPost') }}</button>
                                <button v-bind:disabled="(totalCreditWithTax == totalDebitWithTax) ? false : true" type="button"
                                    v-on:click="OnSubmit('Approved')"
                                    v-if="(isValid('CanAddOC') || isValid('CanEditOC')) && formName == 'OpeningCash'"
                                    class="btn btn-outline-primary  "><i class="far fa-save"></i> {{
                                        $t('AddJournalVoucher.UpdateAsPost') }}</button>

                                <button v-on:click="goToList" class="btn btn-danger "> {{ $t('AddJournalVoucher.Cancel')
                                }}</button>
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        <bulk-attachment :attachmentList="journalVoucher.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>




<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['formName'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                updated: false,
                journalVoucher: {
                    id: '00000000-0000-0000-0000-000000000000',
                    date: '',
                    voucherNumber: '',
                    view: false,
                    comments: '',
                    narration: '',
                    branchId: '',
                    attachmentList:[],
                    isStockTransfer: false
                },
                view: 'false',
                rander: 0,
                oldCode: '',
                journalVoucherItems: [],
                baseUrl: '/JournalVoucher/',
                updateurl: 'UpdatejournalVoucher',
                loading: false,
                VatTaxes: [],
                vatLoad: false,
                showModal: false,
                show: false,
                type: '',
                existjournalVoucher: false,
                totalDebitWithTax: 0,
                totalCreditWithTax: 0,
                requestSending: true,
                renderNote: 0,
                renderVNo: 0,
                language: 'Nothing',
            }
        },
        validations() {
            return {
                journalVoucher: {
                    date: {
                        required
                    },
                    voucherNumber: {
                        required
                    },
                    journalVoucherItems: { required },
                    //journalVoucherItems: {
                    //    required: required,
                    //    minLength: minLength(1),
                    //    isTotalZero: function () {
                    //        if ((this.totalDebitWithTax - this.totalCreditWithTax) == 0) {
                    //            return true;
                    //        }
                    //        return false;
                    //    },
                    //    isDebitAndCreditEqual: function () {
                    //        var isMatch = true;
                    //        this.journalVoucherItems.forEach(function (item) {
                    //            if (item.debit == item.credit) {
                    //                isMatch = false;
                    //            }
                    //        });
                    //        return isMatch;
                    //    }
                    //}

            }
        }
    },
    methods: {
        Attachment: function () {
            this.show = true;
        },

        attachmentSave: function (attachment) {
            this.journalVoucher.attachmentList = attachment;
            this.show = false;
        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },

        languageChange: function (lan) {
            if (this.language == lan) {

                if (this.journalVoucher.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/addjournalvoucher');
                }
                else {

                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        icon: 'error',
                        timer: 4000,
                        timerProgressBar: true,
                    });
                }
            }


        },

        GetAutoCodeGenerator: function (formName) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/JournalVoucher/AutoGenerateCode?formName=' + formName + '&branchId=' + localStorage.getItem('BranchId'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.journalVoucher.voucherNumber = response.data;

                    root.renderVNo++;
                }
            });
        },
        totalCredit: function (value) {
            this.totalCreditWithTax = parseFloat(value).toFixed(3).slice(0, -1);
        },

        totalDebit: function (value) {
            this.totalDebitWithTax = parseFloat(value).toFixed(3).slice(0, -1);
        },

        addModal: function (type) {
            this.showModal = !this.showModal;
            this.type = type;
        },

        itemLoading: function (loading) {
            if (loading === true) {
                /*  document.querySelector(".overlay").style.display = "block";*/
                this.loading = loading;
            } else {
                this.loading = loading;
                /* document.querySelector(".overlay").style.display = "none";*/
            }
        },

        voucherNumber: function (voucherNumber) {
            this.journalVoucher.voucherNumber = voucherNumber;
        },

        updateDate: function (data) {
            this.journalVoucher.date = data;
        },

        updatejournalVoucherItems: function (items) {
            this.journalVoucherItems = items;
        },

        OnSubmit: function (status) {

            localStorage.setItem('active', status);

            var root = this;
            if (this.formName == 'OpeningCash') {
                root.journalVoucher.openingCash = true;
            }
            if (this.formName == 'JournalVoucher') {
                root.journalVoucher.openingCash = false;
            }
            this.loading = true;
            root.journalVoucher.approvalStatus = status
            //root.journalVoucher.date = moment(root.journalVoucher.date).format();
            if (this.formName == 'OpeningCash') {
                var voucherListOp = [];
                root.journalVoucherItems.forEach(function (result) {
                    voucherListOp.push({
                        id: result.id,
                        accountId: result.accountId,
                        paymentMode: 4,
                        paymentMethod: 0,
                        description: result.description,
                        debit: result.debit,
                        credit: result.credit,
                        chequeNo: result.chequeNo,
                        contactId: result.contactId
                    })
                })
                root.journalVoucher.journalVoucherItems = voucherListOp;
                //root.journalVoucher.journalVoucherItems = root.journalVoucherItems;

            }
            else {
                var voucherList = [];

                if (this.$i18n.locale == 'ar') {
                    root.journalVoucherItems.forEach(function (result) {
                        voucherList.push({
                            id: result.id,
                            accountId: result.accountId,
                            description: result.description,
                            paymentMode: result.paymentMode == 'السيولة النقدية' ? 0 : (result.paymentMode == 'مصرف' ? 1 : 4),
                            paymentMethod: result.paymentMethod == 'التحقق من' ? 1 : (result.paymentMethod == 'تحويل' ? 2 : result.paymentMethod == 'الوديعة' ? 3 : 0),
                            debit: result.debit,
                            credit: result.credit,
                            chequeNo: result.chequeNo,
                            contactId: result.contactId
                        })
                    })
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    root.journalVoucherItems.forEach(function (result) {
                        voucherList.push({
                            id: result.id,
                            accountId: result.accountId,
                            description: result.description,
                            paymentMode: result.paymentMode == 'Cash' ? 0 : (result.paymentMode == 'Bank' ? 1 : 4),
                            paymentMethod: result.paymentMethod == 'Cheque' ? 1 : (result.paymentMethod == 'Transfer' ? 2 : result.paymentMethod == 'Deposit' ? 3 : 0),
                            debit: result.debit,
                            credit: result.credit,
                            chequeNo: result.chequeNo,
                            contactId: result.contactId
                        })
                    })
                }


                root.journalVoucher.journalVoucherItems = voucherList;

            }


            this.journalVoucher.branchId = localStorage.getItem('BranchId');

            this.journalVoucher.date = this.journalVoucher.date + " " + moment().format("hh:mm A");
            var request = root.existjournalVoucher == true ? 'UpdateJournalVoucher' : 'AddJvAsync';
            this.requestSending = false;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.journalVoucher.view = this.view;
            this.$https.post(this.baseUrl + request, this.journalVoucher, { headers: { "Authorization": `Bearer ${token}` } }).then(function (data) {
                if (data.data !== null) {
                    if (data.data.message.isSuccess) {
                        if (request != 'AddJournalVoucher') {
                            voucherList = [];
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            if ((root.isValid('CanViewJV') || root.isValid('CanDraftJV') && root.formName == 'JournalVoucher') || (root.isValid('CanDraftOC') || root.isValid('CanViewOC') && root.formName == 'OpeningCash')) {
                                root.$router.push({
                                    path: '/JournalVoucherView',
                                    query: {
                                        data: 'JournalVoucherViews' + root.formName,
                                        formName: root.formName,
                                    }
                                })
                                root.loading = false;
                            }
                            else {
                                root.$router.go();
                                root.loading = false;
                            }



                        }
                        else {
                            root.notifyModel('Updated!', 'Journal Voucher has been Updated', 'success', 'success', root.baseUrl + 'JournalVouchers');
                            if ((root.isValid('CanViewJV') || root.isValid('CanDraftJV') && root.formName == 'JournalVoucher') || (root.isValid('CanDraftOC') || root.isValid('CanViewOC') && root.formName == 'OpeningCash')) {
                                root.$router.push({
                                    path: '/JournalVoucherView',
                                    query: {
                                        data: 'JournalVoucherViews' + root.formName,
                                        formName: root.formName,
                                    }
                                })
                                root.loading = false;
                            }
                            else {
                                root.$router.go();
                                root.loading = false;
                            }
                        }
                    }
                    else {
                        root.loading = false;
                        root.$swal.fire({
                            type: 'error',
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: data.data.message.isAddUpdate,
                            confirmButtonClass: "btn btn-danger",
                            showConfirmButton: true,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    }
                    
                }
                else {
                    root.notifyModel('Error!', 'Journal Voucher' + ' does not Updated.', 'error', 'info');
                    root.loading = false;
                }
            }, function (error) {
                var errorMessage = error.response.data.error;

                root.notifyModel('Error!', errorMessage, 'error', 'danger');
                root.loading = false;
            });
        },
        notifyModel: function (title, text, type, btn, url) {
            var root = this;
            if (url !== undefined) {
                this.$swal({
                    title: title,
                    text: text,
                    type: type,
                    allowOutsideClick: false
                }).then(function () {
                    window.location = url;
                    root.requestSending = true;
                });
            } else {
                this.$swal({
                    title: title,
                    text: text,
                    type: type,
                    allowOutsideClick: false
                }).then(function () {
                    root.requestSending = true;
                });
            }
        },

        goToList: function () {

            var root = this
            if ((root.isValid('CanViewJV') || root.isValid('CanDraftJV') && root.formName == 'JournalVoucher') || (root.isValid('CanDraftOC') || root.isValid('CanViewOC') && root.formName == 'OpeningCash')) {
                root.$router.push({
                    path: '/JournalVoucherView',
                    query: {
                        data: 'JournalVoucherViews' + root.formName,
                        formName: root.formName,
                    }
                })
            }
            else {
                root.$router.go();
            }

        },

        updateStatus: function (value) {
            this.journalVoucher.approvalStatus = value;
            this.renderNote++;
        }
    },
    filters: {
        roundOffFilter: function (value) {

            return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
        }
    },
    created: function () {
        this.$emit('update:modelValue', this.$route.name + this.formName);

        document.querySelector("html").classList.remove("perfect-scrollbar-on");
        var root =  this;
        if(root.$route.query.Add == 'false')
        {
            root.$route.query.data = root.$store.isGetEdit;
        }
    },
    mounted: function () {

        this.language = this.$i18n.locale;
        this.rander++;
        if (this.formName == 'OpeningCash') {


            if (this.$route.query.data == undefined) {


                //this.journalVoucher = this.vm
                //this.journalVoucherItems = this.vm.journalVoucherItems;
                this.existjournalVoucher = this.journalVoucher.id === "00000000-0000-0000-0000-000000000000" ? false : true;
                this.GetAutoCodeGenerator(this.formName);
                if (!this.existjournalVoucher) {
                    this.journalVoucher.date = moment().format('llll');
                } else {
                    this.journalVoucher.date = moment(this.journalVoucher.date).format('llll');
                }
                this.oldCode = this.journalVoucher.voucherNumber;
                if (this.$route.query.view == 'true') {
                    this.view = true;
                }
                else {
                    this.view = false;
                }
            }
            else {

                if (this.$route.query.view == 'true') {
                    this.view = true;
                }
                else {
                    this.view = false;
                }
                this.journalVoucher = this.$route.query.data;


                this.journalVoucherItems = this.$route.query.data.journalVoucherItems;
            }
        }
        if (this.formName == 'JournalVoucher') {


            if (this.$route.query.data == undefined) {


                //this.journalVoucher = this.vm
                //this.journalVoucherItems = this.vm.journalVoucherItems;
                this.existjournalVoucher = this.journalVoucher.id === "00000000-0000-0000-0000-000000000000" ? false : true;
                this.GetAutoCodeGenerator(this.formName);
                if (!this.existjournalVoucher) {
                    this.journalVoucher.date = moment().format('llll');
                } else {
                    this.journalVoucher.date = moment(this.journalVoucher.date).format('llll');
                }
                this.oldCode = this.journalVoucher.voucherNumber;

                if (this.$route.query.view == 'true') {
                    this.view = true;
                }
                else {
                    this.view = false;
                }

            }
            else {


                if (this.$route.query.view == 'true') {
                    this.view = true;
                }
                else {
                    this.view = false;
                }

                this.journalVoucher = this.$route.query.data;
                var voucherListJv = [];

                if (this.$i18n.locale == 'ar') {
                    this.$route.query.data.journalVoucherItems.forEach(function (result) {
                        voucherListJv.push({
                            id: result.id,
                            accountId: result.accountId,
                            description: result.description,
                            paymentMode: result.paymentMode == 0 ? 'السيولة النقدية' : (result.paymentMode == 1 ? 'مصرف' : ''),
                            paymentMethod: result.paymentMethod == 1 ? 'التحقق من' : (result.paymentMethod == 2 ? 'تحويل' : result.paymentMethod == 3 ? 'الوديعة' : ''),
                            debit: result.debit,
                            credit: result.credit,
                            chequeNo: result.chequeNo,
                            contactId: result.contactId
                        })
                    })
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    this.$route.query.data.journalVoucherItems.forEach(function (result) {
                        voucherListJv.push({
                            id: result.id,
                            accountId: result.accountId,
                            description: result.description,
                            paymentMode: result.paymentMode == 0 ? 'Cash' : (result.paymentMode == 1 ? 'Bank' : ''),
                            paymentMethod: result.paymentMethod == 1 ? 'Cheque' : (result.paymentMethod == 2 ? 'Transfer' : result.paymentMethod == 3 ? 'Deposit' : ''),
                            debit: result.debit,
                            credit: result.credit,
                            chequeNo: result.chequeNo,
                            contactId: result.contactId
                        })
                    })
                }

                this.journalVoucherItems = voucherListJv;
                voucherListJv = [];

                //    this.journalVoucherItems = this.$route.query.data.journalVoucherItems;
            }
        }
    }
}</script>