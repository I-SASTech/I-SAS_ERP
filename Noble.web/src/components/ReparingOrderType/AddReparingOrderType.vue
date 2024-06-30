<template>
    <modal :show="show" v-if=" isValid('CanAddWarrantyCategory') || isValid('CanEditWarrantyCategory')  || isValid('CanEditDescription') || isValid('CanEditProblem') || isValid('CanEditAccessory') || isValid('CanAddDescription') || isValid('CanAddProblem') || isValid('CanAddAccessory')">
        <div class="modal-content">
            <div class="modal-header">
                <div v-if="type=='Edit'">
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='WarrantyCategory'">{{ $t('ReparingOrder.UpdateWarrantyCategory') }}</h6>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='UpsDescription'">{{ $t('ReparingOrder.UpdateUpsDescription') }}</h6>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='Problem'">{{ $t('ReparingOrder.UpdateWarrantyProblem') }}</h6>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='AcessoryIncluded'">{{ $t('ReparingOrder.UpdateAcessoryIncluded') }}</h6>
                </div>
                <div v-else>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='WarrantyCategory'">{{ $t('ReparingOrder.AddWarrantyCategory') }}</h6>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='UpsDescription'">{{ $t('ReparingOrder.AddUpsDescription') }}</h6>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='Problem'">{{ $t('ReparingOrder.AddWarrantyProblem') }}</h6>
                    <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="formName=='AcessoryIncluded'">{{ $t('ReparingOrder.AddAcessoryIncluded') }}</h6>

                </div>







                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-if="english=='true'" class="form-group has-label col-lg-12 " v-bind:class="{'has-danger' : v$.reparingOrder.name.$error} && $i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('ReparingOrder.ReparingOrderName'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="v$.reparingOrder.name.$model" type="text" />
                        <span v-if="v$.reparingOrder.name.$error" class="error">
                            <span v-if="!v$.reparingOrder.name.required">{{ $t('reparingOrder.NameRequired') }}</span>
                            <span v-if="!v$.reparingOrder.name.maxLength">{{ $t('reparingOrder.NameLength') }}</span>
                        </span>
                    </div>
                    <div v-if="arabic=='true'" class="form-group has-label col-lg-12 " v-bind:class="{'has-danger' : v$.reparingOrder.nameArabic.$error}">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('ReparingOrder.ReparingOrderName'))  }}: <span class="text-danger"> *</span></label>
                        <input class="form-control  " v-model="v$.reparingOrder.nameArabic.$model" type="text" />
                        <span v-if="v$.reparingOrder.nameArabic.$error" class="error">
                            <span v-if="!v$.reparingOrder.nameArabic.required"> {{ $t('reparingOrder.NameRequired') }}</span>
                            <span v-if="!v$.reparingOrder.nameArabic.maxLength">{{ $t('reparingOrder.NameLength') }}</span>
                        </span>
                    </div>


                    <div class="form-group col-lg-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="reparingOrder.isActive">
                            <label for="inlineCheckbox1"> {{ $t('ReparingOrder.Status') }}</label>
                        </div>
                    </div>
                </div>
            </div>
            <div v-if="!loading">
                <div class="modal-footer " v-if="type=='Edit' && (isValid('CanEditWarrantyCategory') || isValid('CanEditDescription') || isValid('CanEditProblem') || isValid('CanEditAccessory'))">
                    <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveImportExport" v-bind:disabled="v$.reparingOrder.$invalid"> {{ $t('ReparingOrder.btnUpdate') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('ReparingOrder.btnClear') }}</button>
                </div>
                <div class="modal-footer " v-if="type!='Edit' && (isValid('CanAddWarrantyCategory') || isValid('CanAddDescription') || isValid('CanAddProblem') || isValid('CanAddAccessory'))">
                    <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveImportExport" v-bind:disabled="v$.reparingOrder.$invalid"> {{ $t('ReparingOrder.btnSave') }}</button>
                    <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('ReparingOrder.btnClear') }}</button>
                </div>
            </div>
            <div v-else>
                <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
            </div>
          
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'newReparingOrder', 'type', 'formName'],
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                reparingOrder: this.newReparingOrder,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
            return {
                reparingOrder: {
                    name: {
                        maxLength: maxLength(50)
                    },
                    nameArabic: {
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.reparingOrder.name;
                        }),
                        maxLength: maxLength(50)
                    },

                }
                }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },

            SaveImportExport: function () {

                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.reparingOrder.branchId = localStorage.getItem('BranchId');

                this.$https.post('/ReparingOrder/SaveReparingOrderTypeInformation', this.reparingOrder, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: "Your ImportExport Name  Already Exist!",
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
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');


        }
    }
</script>
