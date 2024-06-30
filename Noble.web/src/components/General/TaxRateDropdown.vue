<template>
    <div>
        <select v-bind:disabled="isDisable" v-model="selectedValue" @change="$emit('update:modelValue', selectedValue)" class="form-select" aria-label="Default select example">
            <option value="" disabled selected>{{$t('TaxRateDropdown.SelectVAT/TAX')}}</option>
            <option v-for="item in options" v-bind:key="item.id" :value="item.id">{{item.name}}</option>
        </select>

        <modal :show="show" v-if="show">

            <div class="modal-content">
                <div class="modal-header" v-if="type == 'Edit'">
                    <h5 class="modal-title" id="myModalLabel"> {{ $t('TaxRateDropdown.UpdateTaxRate') }}</h5>
                </div>
                <div class="modal-header" v-else>
                    <h5 class="modal-title" id="myModalLabel"> {{ $t('TaxRateDropdown.AddTaxRate') }}</h5>
                </div>
                <div class="modal-body ">
                    <div class="row ">
                        <div :key="render" class="form-group has-label col-sm-12 "
                             v-bind:class="{ 'has-danger': v$.taxRate.code.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('TaxRateDropdown.Code') }}:</label>
                            <input disabled class="form-control" v-model="v$.taxRate.code.$model" type="text" />
                            <span v-if="v$.taxRate.code.$error" class="error">
                                <span v-if="!v$.taxRate.code.maxLength">{{ $t('TaxRateDropdown.CodeLength') }}</span>
                            </span>
                        </div>
                        <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                             v-bind:class="{ 'has-danger': v$.taxRate.name.$error }">
                            <label class="text  font-weight-bolder">
                                {{$englishLanguage($t('TaxRateDropdown.TaxRateNameEn'))
                                    
                                }}: <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control" v-model="v$.taxRate.name.$model" type="text" />
                            <span v-if="v$.taxRate.name.$error" class="error">
                                <span v-if="!v$.taxRate.name.required">{{ $t('TaxRateDropdown.NameRequired') }}</span>
                                <span v-if="!v$.taxRate.name.maxLength">{{ $t('TaxRateDropdown.NameLength') }}</span>
                            </span>
                        </div>


                        <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                             v-bind:class="{ 'has-danger': v$.taxRate.nameArabic.$error }">
                            <label class="text  font-weight-bolder">
                                {{$arabicLanguage($t('TaxRateDropdown.TaxRateNameAr'))
                                    
                                }}: <span class="text-danger"> *</span>
                            </label>
                            <input class="form-control  "
                                   v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                                   v-model="v$.taxRate.nameArabic.$model" type="text" />
                            <span v-if="v$.taxRate.nameArabic.$error" class="error">
                                <span v-if="!v$.taxRate.nameArabic.required">
                                    {{
 $t('TaxRateDropdown.NameRequired')
                                    }}
                                </span>
                                <span v-if="!v$.taxRate.nameArabic.maxLength">
                                    {{
 $t('TaxRateDropdown.NameLength')
                                    }}
                                </span>
                            </span>
                        </div>


                        <div class="form-group has-label col-sm-12 ">
                            <label class="text  font-weight-bolder">
                                {{ $t('TaxRateDropdown.TaxRate') }}: <span class="text-danger"> *</span>
                            </label>
                            <my-currency-input v-model="taxRate.rate" :isVAT="true"></my-currency-input>
                            <!--<input class="form-control"  v-model="v$.taxRate.rate.$model" type="number" />-->
                            
                        </div>

                        <div class="form-group has-label col-sm-12 "
                             v-bind:class="{ 'has-danger': v$.taxRate.description.$error }">
                            <label class="text  font-weight-bolder"> {{ $t('TaxRateDropdown.Description') }}: </label>
                            <textarea rows="3" class="form-control" v-model="v$.taxRate.description.$model" type="text" />
                            <span v-if="v$.taxRate.description.$error" class="error">
                                {{
                                    $t('TaxRateDropdown.descriptionLength')
                                }}
                            </span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveTaxRate"
                            v-bind:disabled="v$.taxRate.$invalid" v-if="type != 'Edit'">
                        {{
                                $t('TaxRateDropdown.btnSave')
                        }}
                    </button>
                    <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveTaxRate"
                            v-bind:disabled="v$.taxRate.$invalid" v-if="type == 'Edit'">
                        {{
                                $t('TaxRateDropdown.btnUpdate')
                        }}
                    </button>
                    <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                        {{
                            $t('TaxRateDropdown.btnClear')
                        }}
                    </button>
                </div>
            </div>
        </modal>
    </div>
</template>
<script>
    /*import Multiselect from 'vue-multiselect'*/
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'taxdropdown',
        props: ["modelValue", "dropdownpo", "isDisable", "PanelWidth"],
        mixins: [clickMixin],
        //components: {
        //    Multiselect,

        //},
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                selectedValue: [],
                show: false,
                type: '',
                taxRate: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    nameArabic: '',
                    rate: 0,
                    description: '',
                    isActive: true
                },
                render: 0
            }
        },
        validations() {
          return{
              taxRate: {
                name: {
                    maxLength: maxLength(50)
                },
                  nameArabic: {
                      required: requiredIf(function () {
                          return !this.taxRate.name;
                      }),
                    maxLength: maxLength(50)
                },
                rate: {
                    required,
                    maxLength: maxLength(10)
                },
                code: {
                    maxLength: maxLength(30)
                },
                description: {
                    maxLength: maxLength(200)
                }
            }
          }
        },
        methods: {

            GetAmountOfSelected: function () {
                if (this.selectedValue == undefined) {
                    return null;
                }
                else {
                    var vat = this.options.find((value) => value.id == this.selectedValue);
                    if (vat == undefined) {
                        return null;
                    }
                    else {
                        return vat.rate;
                    }
                }
            },

            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/TaxRateList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        response.data.taxRates.forEach(function (result) {
                            if (root.modelValue != undefined && root.modelValue == result.id) {
                                root.selectedValue = result.id;
                            }

                            root.options.push({
                                id: result.id,
                                rate: result.rate,
                                name: (localStorage.getItem('locales') == 'en' || root.isLeftToRight()) ? ((result.name != '' && result.name != null) ? result.name : result.nameArabic) + "(" + result.rate + "%)" : ((result.nameArabic != '' && result.nameArabic != null) ? result.nameArabic : result.name) + "(" + result.rate + "%)"
                            })
                        });

                        root.$store.SetTaxRateList(root.options);
                    }
                });
            },

            AddTax: function (type) {
                
                
                this.GetAutoCodeGenerator();
                this.taxRate = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    name: '',
                    nameArabic: '',
                    rate: 0,
                    description: '',
                    isActive: true
                }

                this.show = !this.show;
                this.type = type;
            },

            close: function () {
                this.show = false;
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
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https.post('/Product/SaveTaxRate', this.taxRate, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.type != "Edit") {
                            root.show = false;
                            root.getData();
                        }
                        else {
                            root.getData();
                            root.show = false;
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your Tax Rate Already Exist!",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: true,
                            timerProgressBar: true,
                        });
                        root.getData();
                    }
                });
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$store.getTaxRateList.length > 0) {
                this.options = this.$store.getTaxRateList;
                if (this.modelValue != undefined) {
                    this.selectedValue = this.modelValue;
                }
            }
            else {
                this.getData();
            }            
        },
    }
</script>
