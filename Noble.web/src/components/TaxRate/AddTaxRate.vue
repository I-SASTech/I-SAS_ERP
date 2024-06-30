<template>
    <modal :show="show" v-if=" isValid('CanAddVatRate') || isValid('CanEditVatRate') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddTaxRate.UpdateTaxRate') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    <span v-if="setup==undefined">
                        {{ $t('AddTaxRate.AddTaxRate') }}
                    </span>
                    <span v-else>
                        {{ $t('AddTaxRate.SetupDefaultVAT') }}
                    </span>
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div :key="render" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.taxRate.code.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddTaxRate.Code') }}:<span class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-model="v$.taxRate.code.$model" type="text" />
                        <span v-if="v$.taxRate.code.$error" class="error">
                            <span v-if="!v$.taxRate.code.maxLength">{{ $t('AddTaxRate.CodeLength') }}</span>
                        </span>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.taxRate.name.$error}">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddTaxRate.TaxRateNameEn'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.taxRate.name.$model" type="text" />
                        <span v-if="v$.taxRate.name.$error" class="error">
                            <span v-if="!v$.taxRate.name.required">{{ $t('AddTaxRate.NameRequired') }}</span>
                            <span v-if="!v$.taxRate.name.maxLength">{{ $t('AddTaxRate.NameLength') }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.taxRate.nameArabic.$error}">
                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('AddTaxRate.TaxRateNameAr'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.taxRate.nameArabic.$model" type="text" />
                        <span v-if="v$.taxRate.nameArabic.$error" class="error">
                            <span v-if="!v$.taxRate.nameArabic.required"> {{ $t('AddTaxRate.NameRequired') }}</span>
                            <span v-if="!v$.taxRate.nameArabic.maxLength">{{ $t('AddTaxRate.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.taxRate.rate.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddTaxRate.TaxRate') }}: <span class="text-danger"> *</span></label>
                        <my-currency-input v-model="v$.taxRate.rate.$model" :isVAT="true"></my-currency-input>
                        <span v-if="v$.taxRate.rate.$error" class="error">
                            <span v-if="!v$.taxRate.rate.required"> {{ $t('AddTaxRate.TaxRateRequired') }}</span>
                            <span v-if="!v$.taxRate.rate.maxLength"> {{ $t('AddTaxRate.TaxRateLength') }}</span>
                        </span>
                    </div>
                    <div class="form-group has-label col-sm-12 " v-if="setup!=undefined">
                        <label class="text  font-weight-bolder">  <span>{{ $t('AddTaxRate.TaxMethod') }} :</span></label>

                        <multiselect :options="options" v-model="taxRate.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddTaxRate.SelectMethod')">
                        </multiselect>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-if="setup==undefined" v-bind:class="{'has-danger' : v$.taxRate.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddTaxRate.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.taxRate.description.$model" type="text" />
                        <span v-if="v$.taxRate.description.$error" class="error">{{ $t('AddTaxRate.descriptionLength') }}</span>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="taxRate.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddTaxRate.Active') }} </label>
                        </div>
                    </div>



                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveTaxRate" v-bind:disabled="v$.taxRate.$invalid" v-if="type!='Edit'">{{ $t('AddTaxRate.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveTaxRate" v-bind:disabled="v$.taxRate.$invalid" v-if="type=='Edit'">{{ $t('AddTaxRate.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddTaxRate.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
    export default {
        components: {
            Multiselect,
            Loading
        },
        mixins: [clickMixin],
        props: ['show', 'newtaxrate', 'type', 'setup'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                loading: false,
                render: 0,
                options: [],
                arabic: '',
                english: '',
                taxRate: {},
            }
        },
        validations() {
            return {
                taxRate: {
                    name: {
                        maxLength: maxLength(50)
                    },
                    nameArabic: {
                        required: requiredIf((x) => {
                            if (x.name == '' || x.name == null)
                                return true;
                            return false;
                        }),
                        maxLength: maxLength(50)
                    }
                    , rate: {
                        required,
                        maxLength: maxLength(10)
                    },
                    code: {
                        maxLength: maxLength(10)
                    },
                    description: {
                        maxLength: maxLength(200)
                    }
                }
            }
        },
        methods: {

            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/TaxRateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.taxRate.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveTaxRate: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/Product/SaveTaxRate', this.taxRate, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.$swal({
                                title: root.$t('AddTaxRate.Saved'),
                                text: root.$t('AddTaxRate.SavedSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1000,
                                timerProgressBar: true,
                            });
                            root.loading = false;
                            root.close();
                            if (root.setup) {
                                root.$emit('TaxSave', true);
                            }
                        }
                        else {
                            root.$swal({
                                title: root.$t('AddTaxRate.Updated'),
                                text: root.$t('AddTaxRate.UpdateSuccessfully'),
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.loading = false;
                            root.close();
                        }
                    }
                    else {
                        root.$swal({
                            title: root.$t('AddTaxRate.Error'),
                            text: root.$t('AddTaxRate.NameAlreadyExist'),
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                        root.loading = false;
                    }
                });
            }
        },
        created: function () {
            
            this.taxRate = this.newtaxrate;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.taxRate.id == '00000000-0000-0000-0000-000000000000' || this.taxRate.id == undefined || this.taxRate.id == '')
                this.GetAutoCodeGenerator();
            //this.options = ['Inclusive', 'Exclusive'];
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.options = ['Inclusive', 'Exclusive'];
            }
            else {
                this.options = ['شامل', 'غير شامل'];
            }

        }
    }
</script>
