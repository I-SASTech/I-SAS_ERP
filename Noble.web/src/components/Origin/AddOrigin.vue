<template>
    <modal :show="show" v-if=" isValid('CanAddOrigin') || isValid('CanEditOrigin') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{ $t('AddOrigin.UpdateProductOrigin') }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('AddOrigin.AddProductOrigin') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div :key="render" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.origin.code.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddOrigin.Code') }}:<span class="text-danger">
                                *</span></label>
                        <input disabled class="form-control"
                            v-model="v$.origin.code.$model" type="text" />
                        <span v-if="v$.origin.code.$error" class="error">
                            <span v-if="!v$.origin.code.maxLength"> {{ $t('AddOrigin.CodeLength') }}</span>
                        </span>
                    </div>
                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.origin.name.$error }">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddOrigin.OriginName'))   }}: <span
                                class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.origin.name.$model"
                            type="text" />
                        <span v-if="v$.origin.name.$error" class="error">
                            <span v-if="!v$.origin.name.required"> {{ $t('AddOrigin.NameRequired') }}</span>
                            <span v-if="!v$.origin.name.maxLength"> {{ $t('AddOrigin.NameLength') }}</span>
                        </span>
                    </div>
                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.origin.nameArabic.$error }">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddOrigin.OriginNameAr'))   }}:
                            <span class="text-danger"> *</span></label>
                        <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                            v-model="v$.origin.nameArabic.$model" type="text" />
                        <span v-if="v$.origin.nameArabic.$error" class="error">
                            <span v-if="!v$.origin.nameArabic.required"> {{ $t('AddOrigin.NameRequired') }}</span>
                            <span v-if="!v$.origin.nameArabic.maxLength">{{ $t('AddOrigin.NameLength') }}</span>
                        </span>
                    </div>


                    <div class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.origin.description.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddOrigin.Description') }}: </label>
                        <textarea rows="3" class="form-control"
                            v-model="v$.origin.description.$model" type="text" />
                        <span v-if="v$.origin.description.$error" class="error"> {{ $t('AddOrigin.descriptionLength')
                        }}</span>
                    </div>



                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="origin.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddOrigin.Active') }} </label>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveOrigin"
                    v-bind:disabled="v$.origin.$invalid" v-if="type != 'Edit' && isValid('CanAddOrigin')">
                    {{ $t('AddOrigin.btnSave') }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveOrigin"
                    v-bind:disabled="v$.origin.$invalid" v-if="type == 'Edit' && isValid('CanEditOrigin')">
                    {{ $t('AddOrigin.btnUpdate') }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                            $t('AddOrigin.btnClear')
                    }}
                </button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>
    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'


import { requiredIf, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
export default {
    mixins: [clickMixin],
    props: ['show', 'newOrigin', 'type'],
    components: {
        Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
    data: function () {
        return {
            origin: this.newOrigin,
            render: 0,
            arabic: '',
            english: '',
            loading: false,
        }
    },
        validations() {
            return {
                origin: {
                    name: {
                        maxLength: maxLength(250)
                    },
                    code: {
                        maxLength: maxLength(30)
                    },
                    nameArabic: {
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.origin.name;
                        }),
                        maxLength: maxLength(250)
                    },
                    description: {
                        maxLength: maxLength(500)
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
            this.$https.get('/Product/OriginCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.origin.code = response.data;
                    root.render++;
                }
            });
        },
        SaveOrigin: function () {
            var root = this;
            var token = '';
            this.loading = true;
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.post('/Product/SaveOrigin', this.origin, { headers: { "Authorization": `Bearer ${token}` } })
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
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
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
                            text: "Your Origin Name  Already Exist!",
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
        if (this.origin.id == '00000000-0000-0000-0000-000000000000' || this.origin.id == undefined || this.origin.id == '')
            this.GetAutoCodeGenerator();

    }
}
</script>
