<template>
    <modal show="show" v-if="isValid('CanAddCOA') || isValid('CanEditCOA') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="$i18n.locale == 'ar' && type=='Add'"> {{ $t('COA.AddAccount') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else-if="$i18n.locale == 'ar' && type=='Edit'"> {{ $t('COA.EditAccount') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{type}} {{ $t('COA.Account') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>

                <!--<span class="modal-title" id="myModalLabel" v-if="$i18n.locale == 'ar' && type=='Add'">  {{ $t('COA.AddAccount') }}</span>
    <span class="modal-title" id="myModalLabel" v-else-if="$i18n.locale == 'ar' && type=='Edit'">{{ $t('COA.EditAccount') }}</span>
    <span class="modal-title" id="myModalLabel" v-else>{{type}} {{ $t('COA.Account') }}</span>-->
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-md-12 " v-if="type=='Add'">
                        <label>{{ $t('COA.AccountType') }}</label>
                        <select v-model="newTypeId" class="form-control" v-if="show">
                            <option value="">{{ $t('COA.SelectaccountType') }}</option>
                            <option v-for="accountType in accounts.accountTypes" v-bind:key="accountType.id" :value="accountType.id">{{ ($i18n.locale == 'en' ||isLeftToRight())?accountType.name:accountType.nameArabic}}</option>
                        </select>
                    </div>
                    <div class="form-group col-md-12 " v-if="type=='Add'">
                        <label>{{ $t('COA.CostCenter') }}</label>
                        <span v-if="newTypeId!== undefined">
                            <select v-model="account.costCenterId" class="form-control" v-if="show">
                                <option value="-1">{{ $t('COA.SelectCostCenter') }}</option>
                                <option v-for="costCenter in costCenters" v-bind:key="costCenter.id" :value="costCenter.id">{{ ($i18n.locale == 'en' ||isLeftToRight())?costCenter.name:costCenter.nameArabic  }}</option>
                            </select>
                        </span>
                    </div>
                    <div class="form-group" v-bind:class="{'has-danger': v$.account.code.$error, 'col-md-12' :type== 'Add' , 'col-md-12':type=='Edit' }" v-if="!CoaCode">
                        <label>{{ $t('COA.CODE') }}:<span class="text-danger"> *</span></label>
                        <input v-model.trim="v$.account.code.$model" class="form-control" />
                        <span v-if="!v$.account.code.required && v$.account.code.$error" class="error validation-error field-validation-valid">{{ $t('COA.CODERequired') }}</span>
                        <span v-if="!v$.account.code.isExist && v$.account.code.$error" class="error validation-error field-validation-valid">{{ $t('COA.CODEExist') }}</span>

                    </div>
                    <div :key="render" class="form-group" v-bind:class="{'has-danger': v$.account.code.$error, 'col-md-12' :type== 'Add' , 'col-md-12':type=='Edit' }" v-if="CoaCode">
                        <label>{{ $t('COA.CODE') }}:<span class="text-danger"> *</span></label>
                        <input v-model="v$.account.code.$model" class="form-control" disabled />
                        <span v-if="!v$.account.code.required && v$.account.code.$error" class="error validation-error field-validation-valid">{{ $t('COA.CODERequired') }}</span>
                        <span v-if="!v$.account.code.isExist && v$.account.code.$error" class="error validation-error field-validation-valid">{{ $t('COA.CODEExist') }}</span>

                    </div>

                    <div v-if="english=='true'" class="form-group" v-bind:class="{'has-danger': v$.account.name.$error, 'col-md-12' :type== 'Add' , 'col-md-12':type=='Edit' }">
                        <label>{{$englishLanguage($t('COA.NAME'))   }}:<span class="text-danger"> *</span></label>
                        <input v-model.trim="v$.account.name.$model" class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" />
                        <span v-if="!v$.account.name.required && v$.account.name.$error" class="error validation-error field-validation-valid">{{ $t('COA.NameRequired') }}</span>

                    </div>
                    <div v-if="isOtherLang()" class="form-group" v-bind:class="{'has-danger': v$.account.nameArabic.$error, 'col-md-12' :type== 'Add' , 'col-md-12':type=='Edit' }">
                        <label>{{$arabicLanguage($t('COA.NAME'))   }}:<span class="text-danger"> *</span></label>
                        <input v-model.trim="v$.account.nameArabic.$model" class="form-control" v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" />
                        <span v-if="!v$.account.nameArabic.required && v$.account.nameArabic.$error" class="error validation-error field-validation-valid">{{ $t('COA.NameRequired') }}</span>

                    </div>
                    <!--<div class="form-group col-md-12" >
        <label >Opening Balance</label>
        <input v-model="account.openingBalance"  @update:modelValue="GetOpeningBalance(account.openingBalance)" class="form-control" type="number" />
    </div>-->
                    <!--<div class="form-group col-md-12">
        <label>Running Balance</label>
        <input v-model="account.runingBalance" disabled class="form-control" type="number" />
    </div>-->

                    <div class="form-group col-md-12 ">
                        <label>{{ $t('COA.Description') }}</label>
                        <input v-model="account.description" class="form-control" />
                    </div>

                    <div class="form-group col-md-12 ms-1">
                        <div class="checkbox">
                            <input id="checkbox0" type="checkbox" v-on:click="account.isActive = !account.isActive">
                            <label for="checkbox0">
                                {{ $t('COA.Active') }}
                            </label>
                        </div>
                    </div>


                </div>
            </div>
            <div class="modal-footer">
                <button v-if="type==='Edit' && isValid('CanEditCOA') " @click="$event.target.disabled = true" type="button" class="btn btn-soft-primary btn-sm" v-on:click="updateAccount(account)" :disabled="v$.account.$invalid"><i class="far fa-save"></i> {{ $t('COA.btnUpdate') }}</button>
                <button v-if="type!='Edit' && isValid('CanAddCOA') " @click="$event.target.disabled = true" type="button" class="btn btn-soft-primary btn-sm" v-on:click="addAccount(account)" :disabled="v$.account.$invalid"><i class="far fa-save"></i> {{ $t('COA.btnSave') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('COA.btnClear') }}</button>
            </div>          
        </div>
    </modal>   
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
    import axios from 'axios';
    export default {
        mixins: [clickMixin],
        props: ['newaccount', 'account-type-id', 'type', 'show'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                accounts: '',
                newTypeId: '',
                oldCode: '',
                render: 0,
                isDisable:false,
                CoaCode:false,
                costCenters: [],
                account: this.newaccount
            }
        },
        validations() {
          return{
              account: {
                code: {
                    required: required,
                    isExist: function (value) {
                        if (value === '' || value === this.oldCode) return true;

                        return new Promise(function (resolve, reject) {

                            resolve(
                                axios.get('/Accounting/IsAccountExist?code=' + value)
                                    .then(function (response) {
                                        if (response.data.value) {
                                            console.log("isExist");
                                            return false;
                                        } else {
                                            console.log("Not isExist");
                                            return true;

                                        }
                                    },
                                        function () {
                                            console.log(" not isExist");
                                            return true;

                                        })
                            );

                            reject(console.log("ddd"));

                        });
                    }
                },
                name: {
                    maxLength: maxLength(50)
                },
                nameArabic: {
                    required: requiredIf(function () {
                            return !this.account.name;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.name == '' || x.name == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
            }
          }
        },
        methods: {
            GetOpeningBalance: function () {
                this.account.runingBalance = this.account.openingBalance;
            },
            updateAccount: function (account) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.post('/Accounting/UpdateAccount', account, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.value === true) {
                            var data = root.accounts.accountTypes.find(function (x) {
                                return x.id === root.accountTypeId;
                            }).costCenters.find(function (y) {
                                return y.id === root.account.costCenterId;
                            }).accounts
                                .find(function (z) { return z.id === response.data.id });
                            data.code = root.account.code;
                            data.name = root.account.name;
                            data.nameArabic = root.account.nameArabic;
                            data.description = root.account.description;
                            data.isActive = root.account.isActive;
                            data.openingBalance = root.account.openingBalance;
                            data.runingBalance = root.account.runingBalance;
                            root.$swal({
                                title: root.$t('COA.Updated'),
                                text: root.$t('COA.AccountUpdated'),
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });
                            root.$emit('close');
                        } else {
                            root.$emit('close');
                            this.$swal({
                                title: root.$t('COA.Error'),
                                text: root.$t('COA.AccountNotUpdated'),
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });
                        }
                    },
                        function (error) {
                            root.$emit('close');
                            root.$swal({
                                title: root.$t('COA.Error'),
                                text: error,
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });
                        });
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Accounting/AccountCode?Id=' + this.account.costCenterId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.account.code = response.data;
                        root.render++;
                    }
                });
            },
            addAccount: function (account) {
                account.id = "00000000-0000-0000-0000-000000000000";
                var root = this;
                  
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.post('/Accounting/AddAccount', account, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.value === true) {


                            root.accounts.accountTypes.find(function (x) {
                                return x.id === root.newTypeId;
                            }).costCenters.find(function (y) {
                                return y.id === root.account.costCenterId;
                            }).accounts.push({
                                costCenterId: root.account.costCenterId,
                                code: root.account.code,
                                description: root.account.description,
                                id: response.data.id,
                                isActive: root.account.isActive,
                                runingBalance: root.account.runingBalance,
                                openingBalance: root.account.openingBalance,
                                nameArabic: root.account.nameArabic,

                                name: root.account.name
                            });


                            root.$swal({
                                title: root.$t('COA.Saved'),
                                text: root.$t('COA.AccountAdd'),
                                type: 'success',
                                confirmButtonClass: "btn btn-success",
                                buttonsStyling: false
                            });
                            root.$emit('close');

                        } else {

                            root.$swal({
                                title: root.$t('COA.Error'),
                                text: root.$t('COA.AccountNotaddSucessfully'),
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });

                            root.$emit('close');
                        }
                    },
                        function (error) {
                            root.$emit('close');

                            root.$swal({
                                title: root.$t('COA.Error'),
                                text: error,
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });
                        });
            },

            close: function () {
                this.$emit('close');
            }
        },

        created: function () {

            //compatibilty with IE
            this.oldCode = this.account.code;
            this.newTypeId = this.accountTypeId;

        },
        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.CoaCode = localStorage.getItem('coaCode') == 'true' ? true : false;

            this.arabic = localStorage.getItem('Arabic');
            if (this.type == 'Add') {
                if (this.CoaCode) {
                    this.GetAutoCodeGenerator();

                }

            }
            this.accounts = this.$store.isaccounts;
            if (this.account.openingBalance > 0) {
                this.isDisable = true;
            } else {
                this.isDisable = false;

            }


        },
        watch: {
            account: function (a) {
                //compatibilty with chrome
                this.oldCode = a.code;
            },
            newTypeId: function (newTypeId) {
                this.costCenters = this.accounts.accountTypes.find(function (x) { return x.id == newTypeId; }).costCenters;
            }
        }

    }
</script>