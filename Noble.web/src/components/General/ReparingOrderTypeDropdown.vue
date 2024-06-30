<template>
    <div>
        
        <multiselect v-model="DisplayValue" tag-placeholder="Add New"
                     ref="multiselect"
                     v-bind:disabled="disable"
                     class="accountDropPanel"
                     @open="open"
                     label="name" track-by="name"
                     :options="options" :multiple="false" :taggable="true"
                     @tag="AddCCToList"></multiselect>

        <modal :show="show" v-if="show">

            <div style="margin-bottom:0px" class="card" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" v-if="type=='Edit'">

                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='WarrantyCategory'">{{ $t('ReparingOrder.UpdateWarrantyCategory') }} </h5>
                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='UpsDescription'">{{ $t('ReparingOrder.UpdateUpsDescription') }} </h5>
                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Problem'">{{ $t('ReparingOrder.UpdateWarrantyProblem') }} </h5>
                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='AcessoryIncluded'">{{ $t('ReparingOrder.UpdateAcessoryIncluded') }} </h5>


                            </div>
                            <div class="modal-header" v-else>

                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='WarrantyCategory'">{{ $t('ReparingOrder.AddWarrantyCategory') }} </h5>
                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='UpsDescription'">{{ $t('ReparingOrder.AddUpsDescription') }} </h5>
                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='Problem'">{{ $t('ReparingOrder.AddWarrantyProblem') }} </h5>
                                <h5 class="modal-title DayHeading" id="myModalLabel" v-if="formName=='AcessoryIncluded'">{{ $t('ReparingOrder.AddAcessoryIncluded') }} </h5>


                            </div>
                            <div>
                                <div class="card-body ">
                                    <div class="row ">

                                        <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.reparingOrder.name.$error} && $i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            <label class="text  font-weight-bolder"> {{$englishLanguage($t('ReparingOrder.ReparingOrderName'))  }}: <span class="text-danger"> *</span></label>
                                            <input class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.reparingOrder.name.$model" type="text" />
                                            <span v-if="v$.reparingOrder.name.$error" class="error">
                                                <span v-if="!v$.reparingOrder.name.required">{{ $t('reparingOrder.NameRequired') }}</span>
                                                <span v-if="!v$.reparingOrder.name.maxLength">{{ $t('reparingOrder.NameLength') }}</span>
                                            </span>
                                        </div>
                                        <div v-if="arabic=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.reparingOrder.nameArabic.$error}">
                                            <label class="text  font-weight-bolder"> {{$arabicLanguage($t('ReparingOrder.ReparingOrderName'))  }}: <span class="text-danger"> *</span></label>
                                            <input class="form-control arabicLanguage " v-model="v$.reparingOrder.nameArabic.$model" type="text" />
                                            <span v-if="v$.reparingOrder.nameArabic.$error" class="error">
                                                <span v-if="!v$.reparingOrder.nameArabic.required"> {{ $t('reparingOrder.NameRequired') }}</span>
                                                <span v-if="!v$.reparingOrder.nameArabic.maxLength">{{ $t('reparingOrder.NameLength') }}</span>
                                            </span>
                                        </div>



                                        <div class="form-group col-md-12" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                            <label style="margin: 7px;">{{ $t('ReparingOrder.Status') }}</label> <br />
                                            <div class="bootstrap-switch bootstrap-switch-wrapper bootstrap-switch-animate" v-bind:class="{'bootstrap-switch-on': reparingOrder.isActive, 'bootstrap-switch-off': !reparingOrder.isActive}" v-on:click="reparingOrder.isActive = !reparingOrder.isActive" style="width: 72px;">
                                                <div class="bootstrap-switch-container" style="width: 122px; margin-left: 0px;">
                                                    <span class="bootstrap-switch-handle-on bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-check-2"></i>
                                                    </span>
                                                    <span class="bootstrap-switch-label" style="width: 30px;">&nbsp;</span>
                                                    <span class="bootstrap-switch-handle-off bootstrap-switch-success" style="width: 50px;">
                                                        <i class="nc-icon nc-simple-remove"></i>
                                                    </span>
                                                    <input class="bootstrap-switch" type="checkbox" data-toggle="switch" checked="" data-on-label="<i class='nc-icon nc-check-2'></i>" data-off-label="<i class='nc-icon nc-simple-remove'></i>" data-on-color="success" data-off-color="success">
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                            <div>

                                <div class="modal-footer justify-content-right">
                                    <button type="button" class="btn btn-primary  " v-on:click="SaveImportExport" v-bind:disabled="v$.reparingOrder.$invalid"> {{ $t('ReparingOrder.btnSave') }}</button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('ReparingOrder.btnClear') }}</button>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                 <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            </div>
        </modal>

    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
    import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        name: 'ReparingOrderTypeDropdown',
        props: ["modelValue", "formName", "disable", "isWarranty", "update"],

        components: {
            Multiselect,
            Loading
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                options: [],
                value: '',
                id: '',
                show: false,
                type: '',

                render: 0,
                arabic: '',
                english: '',
                searchQuery: '',
                reparingOrderlist: [],
                reparingOrder: {
                    id: '',
                    name: '',
                    nameArabic: '',
                    description: '',
                    reparingOrderTypes: '',
                    code: '',
                    isActive: true,
                    branchId: '',
                },
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
            }
        },
        validations() {
           return{
             reparingOrder: {
                name: {
                    maxLength: maxLength(50)
                },
                nameArabic: {
                    required: requiredIf(function () {
                            return !this.reparingOrder.name;
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
            open() {

                this.$nextTick(() => {

                    if (this.value.name != undefined) {
                        //this.$refs.multiselect.search = this.value.name
                        this.$refs.multiselect.placeholder = this.value.name
                    }

                });
            },
            AddBrand: function () {

                this.v$.$reset();
                this.reparingOrder = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    reparingOrderTypes: this.formName,
                    isActive: true

                }

                this.show = !this.show;
            },
            close: function () {
                this.show = false;
            },
            AddCCToList: function (newEmail) {

                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.reparingOrder.id = '00000000-0000-0000-0000-000000000000';
                this.reparingOrder.name = newEmail;
                this.reparingOrder.reparingOrderTypes = this.formName;
                this.$https.post('/ReparingOrder/SaveReparingOrderTypeInformation', this.reparingOrder, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.id) {


                            {


                                root.getData();
                                root.modelValue = response.data.id;

                                root.close();
                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: "Duplicate Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)

            },
            SaveImportExport: function (newEmail) {

                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                alert(this.value);
                this.reparingOrder.name = newEmail;
                this.reparingOrder.reparingOrderTypes = this.formName;
                this.reparingOrder.branchId = localStorage.getItem('BranchId');

                this.$https.post('/ReparingOrder/SaveReparingOrderTypeInformation', this.reparingOrder, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {


                            {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: "Saved Sucessfully!",
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.getData();
                                root.close();
                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: "Duplicate Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },

            getData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var branchId = localStorage.getItem('BranchId');

                root.options = [];
                this.$https.get('/ReparingOrder/ReparingOrderTypeList?isDropdown=true' + '&ReparingOrderTypes=' + this.formName + '&branchId=' + branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: root.$i18n.locale == 'en' ? (result.name != '' ? result.name : result.nameArabic) : (result.nameArabic != '' ? result.nameArabic : result.name),
                            });
                            if (root.update == true && root.isWarranty == true) {
                                if (root.modelValue == null || root.modelValue == '' || root.modelValue == undefined) {
                                    root.modelValue = result.id;
                                }
                            }
                            // else if (root.isWarranty == true) {
                                
                            //     if (result.name == 'No Warranty')
                            //     {
                            //         root.modelValue = result.id;
                            //     }
                                    
                            // }

                        })


                    }
                }).then(function () {
                    if (root.modelValue != undefined && root.modelValue != '') {

                        root.options.forEach(function (x) {
                            if (x.id === root.modelValue) {

                                root.value = x;

                                root.$emit('update:modelValue', x.id);
                            }
                        })
                    }

                });
            },
        },
        computed: {
            DisplayValue: {
                get: function () {

                    if (this.value != "" || this.value != undefined) {

                        return this.value;
                    }
                    return this.value;
                },
                set: function (value) {

                    this.value = value;
                    this.$emit('update:modelValue', value.id);
                }
            }
        },
        mounted: function () {
            this.getData();
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        },
    }
</script>