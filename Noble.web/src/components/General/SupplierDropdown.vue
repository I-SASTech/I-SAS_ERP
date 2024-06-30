<template>
    <div>
        <multiselect v-model="DisplayValue" v-if="disable" :options="options" :multiple="false" track-by="name"
            :clear-on-select="false" :show-labels="false" label="name" disabled
            v-bind:placeholder="$t('SupplierDropdown.SelectSupplier')" :preselect-first="true">
            
                    <template v-slot:noResult>
                        <a  class="btn btn-primary " v-on:click="AddCustomer()" v-if="isValid('CanAddSupplier')">{{
                    $t('SupplierDropdown.CreateQuickSupplier')}}</a><br />
                    </template>
        </multiselect>
        <multiselect v-model="DisplayValue" v-else :options="options" :multiple="false" track-by="name"
            :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('SupplierDropdown.SelectSupplier')"
            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '"
            :preselect-first="true">
            <template v-slot:noResult>
                        <a  class="btn btn-primary " v-on:click="AddCustomer()" v-if="isValid('CanAddSupplier')">{{
                    $t('SupplierDropdown.CreateQuickSupplier')}}</a><br />
                    </template>
        </multiselect>

        <modal :show="show" v-if="show">

            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="myModalLabel"> {{ $t('SupplierDropdown.AddSupplier') }}</h5>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SupplierDropdown.SupplierCode') }} :<span class="text-danger">
                                    *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.customer.code.$error }">
                                <input readonly class="form-control " v-model="v$.customer.code.$model" />
                                <span v-if="v$.customer.code.$error" class="error text-danger">
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SupplierDropdown.SupplierType') }} :<span class="text-danger">
                                    *</span></label>
                            <multiselect style="z-index:999" v-model="customer.supplierType"
                                v-if="($i18n.locale == 'en' || isLeftToRight())"
                                :options="['Wholesaler', 'Retailer', 'Wholesaler & Retailer', 'Dealer', 'Distributor', 'International Supplier', 'International Manufacturers', 'International Agent / Exporter']"
                                :show-labels="false" v-bind:placeholder="$t('SelectOption')"> </multiselect>
                            <multiselect style="z-index:999" v-model="customer.supplierType" v-else
                                :options="['جمله', 'قطاعي', 'بائع بالجملة', 'وكيل', 'موزع', 'مزود دولي', 'الشركات المصنعة العالمية', 'وكيل / مصدر دولي']"
                                :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                            </multiselect>
                        </div>
                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SupplierDropdown.PaymentTerms') }} :<span class="text-danger">
                                    *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.customer.paymentTerms.$error }">
                                <multiselect v-model="v$.customer.paymentTerms.$model"
                                    v-if="($i18n.locale == 'en' || isLeftToRight())" :options="['Cash', 'Credit']"
                                    :show-labels="false" placeholder="Select Type">
                                </multiselect>
                                <multiselect v-else v-model="v$.customer.paymentTerms.$model" :options="['نقد', 'آجل']"
                                    :show-labels="false" v-bind:placeholder="$t('SelectOption')">
                                </multiselect>
                                <span v-if="v$.customer.paymentTerms.$error" class="error text-danger">
                                    <span v-if="!v$.customer.paymentTerms.required">{{
                                            $t('SupplierDropdown.PaymentValidation')
                                    }} </span>
                                </span>
                            </div>
                        </div>
                        <div v-if="english == 'true'" class="col-sm-12 form-group">
                            <label>{{$englishLanguage($t('SupplierDropdown.SupplierName(English)'))   }} :<span
                                    class="text-danger"> *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.customer.englishName.$error }">
                                <input class="form-control " @update:modelValue="DisplayName()" v-model="v$.customer.englishName.$model" />
                                <span v-if="v$.customer.englishName.$error" class="error text-danger">
                                    <span v-if="!v$.customer.englishName.required">{{
                                            $t('SupplierDropdown.EngValidation')
                                    }} </span>
                                    <span v-if="!v$.customer.englishName.maxLength">{{ $t('SupplierDropdown.EngMax')
                                    }} </span>
                                </span>
                            </div>
                        </div>
                        <div v-if="isOtherLang()" class="col-sm-12 form-group">
                            <label>{{$arabicLanguage($t('SupplierDropdown.SupplierName(Arabic)'))   }} :<span
                                    class="text-danger"> *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.customer.arabicName.$error }">
                                <input class="form-control " @update:modelValue="DisplayName()"
                                    v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                                    v-model="v$.customer.arabicName.$model" />
                                <span v-if="v$.customer.arabicName.$error" class="error text-danger">

                                    <span v-if="!v$.customer.arabicName.maxLength">{{ $t('SupplierDropdown.ArMax')
                                    }} </span>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-12">
                        <label>{{ $t('AddCustomer.CustomerDisplayName') }} :<span class="text-danger"> *</span></label>
                        <display-name-dropdown v-model="customer.customerDisplayName" :modelValue="customer.customerDisplayName" :newCustomer="customer" :key="salutatioRender" />

                    </div>
                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SupplierDropdown.RegistrationDate') }} :</label>
                            <div v-bind:class="{ 'has-danger': v$.customer.registrationDate.$error }">
                                <datepicker v-model="v$.customer.registrationDate.$model"></datepicker>
                                <span v-if="v$.customer.registrationDate.$error" class="error text-danger">
                                </span>
                            </div>
                        </div>

                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SupplierDropdown.VAT/NTN/Tax No') }} :<span class="text-danger">
                                    *</span></label>
                            <div v-bind:class="{ 'has-danger': v$.customer.vatNo.$error }">
                                <input class="form-control " v-model="v$.customer.vatNo.$model" />
                                <span v-if="v$.customer.vatNo.$error" class="error text-danger">
                                    <span v-if="!v$.customer.vatNo.required">{{ $t('SupplierDropdown.VatNoRequired')
                                    }}</span>
                                </span>
                            </div>
                        </div>

                        <div class="col-sm-12 form-group">
                            <label>{{ $t('SupplierDropdown.Telephone') }} :</label>
                            <input class="form-control " v-model="customer.telephone" />
                        </div>
                        <div class="col-sm-12">
                        <label >Supplier Code :</label>
                        <input class="form-control "  v-model="customer.customerCode" />

                    </div>

                        <div class="col-sm-12 form-group">
                            <label>Address:</label>
                            <div>
                                <textarea rows="3" class="form-control " v-model="customer.address" />
                            </div>
                        </div>
                        <div class="form-group col-md-4" v-if="isRaw == 'true'">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox1" v-model="customer.isRaw">
                                <label for="inlineCheckbox1"> {{ $t('SupplierDropdown.RawSupplier') }} : </label>
                            </div>
                        </div>
                        <!-- <div class="col-sm-6 mt-3" v-if="isRaw == 'true'">
                                <label class="text  font-weight-bolder "> {{ $t('SupplierDropdown.RawSupplier') }}
                                    :</label>
                                <toggle-button v-model="customer.isRaw" class="ml-2 mt-2" color="#3178F6" />
                            </div> -->
                    </div>
                </div>

                <div class="modal-footer justify-content-right">
                    <button type="button" class="btn btn-primary  " v-on:click="SaveCustomer"
                        v-bind:disabled="v$.customer.$invalid"> {{ $t('SupplierDropdown.btnSave') }}</button>
                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{
                            $t('SupplierDropdown.btnClear')
                    }}</button>
                </div>
                 <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>

        </modal>

    </div>
</template>
<script>
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
import clickMixin from '@/Mixins/clickMixin'
import moment from 'moment';
import Multiselect from "vue-multiselect";
import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
export default {
    name: "SupplierDropdown",
    props: ["modelValue", "status", 'disable', "paymentTerm"],
    mixins: [clickMixin],
    components: {
        Multiselect,
        Loading
    },
     setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
             loading: false,
            paymentTerms: '',
            isRaw: '',
            arabic: '',
            english: '',
            options: [],
            value: "",
            show: false,
            customer: {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                supplierType: '',
                registrationDate: '',
                englishName: '',
                arabicName: '',
                customerDisplayName: '',
                vatNo: '',
                address: '',
                customerCode: '',
                telephone: '',
                isRaw: false,
                isCustomer: false,
                isActive: true,
                paymentTerms: '',
                prefix: '',
            },
        };
    },
    methods: {
        DisplayName: function () {
            this.salutatioRender++;
        },
        EmptyRecord: function () {
                
                this.DisplayValue='';

            
            },
        AddCustomer: function () {
            this.v$.$reset();
            this.customer = {
                id: '00000000-0000-0000-0000-000000000000',
                code: '',
                supplierType: '',
                paymentTerms: '',
                registrationDate: '',
                englishName: '',
                arabicName: '',
                customerDisplayName: '',
                vatNo: '',
                address: '',
                customerCode: '',
                prefix: '',
                telephone: '',
                isRaw: false,
                isCustomer: false,
                isActive: true
            }
            this.GetAutoCodeGenerator();
            this.customer.registrationDate = moment().format('llll');
            this.show = !this.show;
        },
        SaveCustomer: function () {
            this.loading = true;
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            if (this.customer.supplierType == 'جمله' || this.customer.supplierType == 'Wholesaler') {
                this.customer.supplierType = 1;
            }
            else if (this.customer.supplierType == 'قطاعي' || this.customer.supplierType == 'Retailer') {
                this.customer.supplierType = 2;
            }
            else if (this.customer.supplierType == 'بائع بالجملة' || this.customer.supplierType == 'Wholesaler & Retailer') {
                this.customer.supplierType = 5;
            }
            else if (this.customer.supplierType == 'وكيل' || this.customer.supplierType == 'Dealer') {
                this.customer.supplierType = 3;
            }
            else if (this.customer.supplierType == 'موزع' || this.customer.supplierType == 'Distributor') {
                this.customer.supplierType = 4;
            }
            else if (this.customer.supplierType == 'International Supplier' || this.supplier.supplierType == 'مزود دولي') {
                this.customer.supplierType = 6;
            }
            else if (this.customer.supplierType == 'International Manufacturers' || this.supplier.supplierType == 'الشركات المصنعة العالمية') {
                this.customer.supplierType = 7;
            }
            else if (this.customer.supplierType == 'International Agent / Exporter' || this.supplier.supplierType == 'وكيل / مصدر دولي') {
                this.customer.supplierType = 8;
            }
            else {
                console.log(this.customer.supplierType);
            }
            root.$https
                .post('/Contact/SaveContact', this.customer, { headers: { "Authorization": `Bearer ${token}` } })
                .then(response => {
                    if (response.data.message.id != '00000000-0000-0000-0000-000000000000' && response.data.action == "Add") {
                        root.loading = false
                        root.info = response.data.bpi
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true
                        });
                        root.show = !root.show;
                        this.getData(true);
                    }
                })
                .catch(error => {
                    console.log(error)
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: error.response.data,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    root.show = !root.show;
                    root.loading = false
                })
                .finally(() => root.loading = false)
        },
        GetAutoCodeGenerator: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https
                .get('/Contact/AutoGenerateCode?issupplier=false'+ '&isCashCustomer=' + false, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.customer.code = response.data.contact;

                    }
                });
        },
        close: function () {
            this.show = false;
        },
        getData: function (quick) {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }
            if (quick) {
                root.options = [];
            }

            var supplier = this.status == undefined ? false : this.status;
            this.paymentTerms = this.paymentTerm ? 'Credit' : '';

            this.$https
                .get('/Contact/ContactList?IsDropDown=' + true + '&isCustomer=' + false + '&isActive=' + true + '&status=' + supplier + '&paymentTerms=' + this.paymentTerms, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then(function (response) {
                    

                    if (response.data != null) {
                        response.data.results.forEach(function (sup) {
                            if (sup.contactNo1 == null)
                            sup.contactNo1 = '';
                            if (sup.customerCode == null)
                            sup.customerCode = '';
                            var displayName = '';
                        if (sup.customerDisplayName != null && sup.customerDisplayName != "") {
                            displayName = '\u202A' + sup.customerDisplayName + '\u202C' + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + sup.contactNo1+sup.customerCode;
                        } else if (sup.englishName != '' && sup.englishName != null) {
                            displayName = sup.englishName + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + sup.contactNo1+sup.customerCode;
                        } else if (sup.arabicName != '' && sup.arabicName != null) {
                            displayName = '\u202A' + sup.arabicName + '\u202C' + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + sup.contactNo1+sup.customerCode;
                        } else {
                            displayName = sup.englishName+sup.customerCode;
                        }
                            root.options.push({
                                id: sup.id,
                                name: displayName,
                            });
                        });
                    }
                })
                .then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.modelValue;
                    });
                });
        },
    },
    validations() {
        return{
            customer: {
            code: { required },
            registrationDate: { required },
            englishName: {
                maxLength: maxLength(30)
            },
            arabicName: {
                required: requiredIf(function () {
                            return !this.customer.englishName;
                        }),
                // required: requiredIf((x) => {
                //     if (x.englishName == '' || x.englishName == null)
                //         return true;
                //     return false;
                // }),
                maxLength: maxLength(40)
            },
            customerDisplayName: {
                required
            },
            vatNo: {
                required
            },
            paymentTerms: {
                required
            },
        }
        }
    },

    computed: {
        DisplayValue: {
            get: function () {
                if (this.value != "" || this.value != undefined) {
                    return this.value;
                }
                return this.modelValue;
            },
            set: function (value) {
                if (value != null) {
                    this.value = value;
                    this.$emit("update:modelValue", value.id);
                }
                else {
                    this.value = value;
                    this.$emit("update:modelValue", null);
                }
            },
        },
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.isRaw = localStorage.getItem('IsProduction');
        this.getData();
    },
};
</script>