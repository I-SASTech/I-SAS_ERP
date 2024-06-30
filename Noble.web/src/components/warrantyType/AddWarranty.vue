<template>
    <modal :show="show" v-if=" isValid('CanAddWarrantyType') || isValid('CanEditWarrantyType') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('WarrantyType.UpdateWarrantyType') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('WarrantyType.AddWarrantyType') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('WarrantyType.WarrantyTypeEnglish') }}: <span class="text-danger"> *</span></label>
                        <input class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="warrantyType.name" type="text" />
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('WarrantyType.WarrantyTypeArabic') }}: <span class="text-danger"> *</span></label>
                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="warrantyType.nameArabic" type="text" />
                    </div>

                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model=" warrantyType.isActive">
                            <label for="inlineCheckbox1"> {{ $t('WarrantyType.Status') }} </label>
                        </div>
                    </div>




                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.warrantyType.$invalid" v-if="type!='Edit' && isValid('CanAddWarrantyType')">{{ $t('WarrantyType.Save') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.warrantyType.$invalid" v-if="type=='Edit' && isValid('CanEditWarrantyType')">{{ $t('WarrantyType.Update') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('WarrantyType.Cancel') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { maxLength, requiredIf } from '@vuelidate/validators';
     import useVuelidate from '@vuelidate/core'

     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        props: ['show', 'warrantytype', 'type'],
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                warrantyType: {},
            }
        },
        validations() {
            return {
                warrantyType: {
                    name: {
                        maxLength: maxLength(250)
                    },
                    nameArabic: {
                        required: requiredIf(function () {
                            return !this.warrantyType.name;
                        }),
                        maxLength: maxLength(250)
                    }
                }
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveColor: function () {
                var root = this;
                this.loading = true;


                this.$https.post('/Product/SaveWarrantyType', this.warrantyType, { headers: { "Authorization": `Bearer ${localStorage.getItem('token')}` } })
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
                                text: "Your Color Name  Already Exist!",
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
        created: function () {
            this.warrantyType = this.warrantytype;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
