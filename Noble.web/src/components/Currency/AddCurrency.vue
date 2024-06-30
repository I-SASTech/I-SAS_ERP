<template>
    <modal :show="show" v-if=" isValid('CanAddCurrency') || isValid('CanEditCurrency') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{ $t('AddCurrency.UpdateCurrency')}}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    <span v-if="setup == undefined">{{ $t('AddCurrency.AddCurrency') }}</span>
                    <span v-else>{{ $t('AddCurrency.SetupDefaultCurrency') }}</span>

                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-if="english == 'true'" class="form-group col-sm-12">
                        <label>
                            {{$englishLanguage($t('AddCurrency.CurrencyName'))   }} :<span class="text-danger">
                                *
                            </span>
                        </label>
                        <div v-bind:class="{ 'has-danger': v$.currency.name.$error }">
                            <input :disabled="currency.isDisable" class="form-control" v-model="v$.currency.name.$model"
                                   type="text" />
                            <span v-if="v$.currency.name.$error" class="error text-danger">
                                <span v-if="!v$.currency.name.required">{{ $t('AddCurrency.NameRequired') }}</span>
                                <span v-if="!v$.currency.name.maxLength">{{ $t('AddCurrency.NameLength') }}</span>
                            </span>
                        </div>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12  "
                         v-bind:class="{ 'has-danger': v$.currency.nameArabic.$error }">
                        <label class="text  font-weight-bolder">
                            {{$arabicLanguage($t('AddCurrency.CurrencyName'))   }}:
                            <span class="text-danger"> *</span>
                        </label>
                        <input :disabled="currency.isDisable" class="form-control "
                               v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'"
                               v-model="v$.currency.nameArabic.$model" type="text" />
                        <span v-if="v$.currency.nameArabic.$error" class="error">
                            <span v-if="!v$.currency.nameArabic.required"> {{ $t('AddCurrency.NameRequired') }}</span>
                            <span v-if="!v$.currency.nameArabic.maxLength">{{ $t('AddCurrency.NameLength') }}</span>
                        </span>
                    </div>



                    <div v-if="english == 'true' || (english == 'false' && isOtherLang())" class="form-group col-sm-12">
                        <label>{{ $t('AddCurrency.SIGN') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{ 'has-danger': v$.currency.sign.$error }">
                            <input :disabled="currency.isDisable" class="form-control" v-model="v$.currency.sign.$model"
                                   type="text" />
                            <span v-if="v$.currency.sign.$error" class="error text-danger">
                                <span v-if="!v$.currency.sign.required">{{ $t('AddCurrency.SignRequired') }}</span>
                                <span v-if="!v$.currency.sign.maxLength">{{ $t('AddCurrency.SignMax') }}</span>
                            </span>
                        </div>
                    </div>
                    <div v-if="isOtherLang()" class="form-group col-sm-12">
                        <label>{{ $t('AddCurrency.ArabicSign') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{ 'has-danger': v$.currency.arabicSign.$error }">
                            <input :disabled="currency.isDisable" class="form-control"
                                   v-model="v$.currency.arabicSign.$model" type="text"
                                   v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'arabicLanguage' : 'arabicLanguage'" />
                            <span v-if="v$.currency.arabicSign.$error" class="error text-danger">
                                <span v-if="!v$.currency.arabicSign.required">
                                    {{$t('AddCurrency.SignRequired') }}
                                </span>
                                <span v-if="!v$.currency.arabicSign.maxLength">{{ $t('AddCurrency.SignMax') }}</span>
                            </span>
                        </div>
                    </div>
                    <div class="form-group col-sm-12" v-if="setup == undefined">
                        <div :key="renderImg">
                            <div class="input-group mb-3" v-if="!((imageSrc == '' && currency.image != '') || (imageSrc != '' && currency.image == '') || (imageSrc != '' && currency.image != ''))">
                                <input ref="imgupload" type="file" class="form-control" id="inputGroupFile02" @change="uploadImage()" accept="image/*"
                                       name="image" >
                            </div>

                            <div class="text-right " v-if="imageSrc != ''">
                                <img v-if="imageSrc != ''" class="float-right" :src="imageSrc" width="100" />
                            </div>
                            <div v-else class="text-right ">
                                <img v-if="currency.image != null && currency.image != ''" class="float-right" :src="'data:image/png;base64,' + currency.image" width="100" />
                            </div>
                        </div>
                    </div>
                    <div class="form-group col-sm-12" v-if="imageSrc != '' || currency.image != ''">
                        <div class="text-right">
                            <button class="btn btn-danger  btn-sm" v-on:click="removeImage()">{{ $t('AddCurrency.Remove') }}</button>
                        </div>
                    </div>
                    <div class="form-group col-md-4" v-if="setup == undefined">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="currency.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddCurrency.Active') }} </label>
                        </div>
                    </div>


                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCurrency"
                        v-bind:disabled="v$.currency.$invalid" v-if="type != 'Edit' ">
                    {{
                            $t('AddCurrency.btnSave')
                    }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCurrency"
                        v-bind:disabled="v$.currency.$invalid" v-if="type == 'Edit'">
                    {{
                            $t('AddCurrency.btnUpdate')
                    }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                        $t('AddCurrency.btnClear')
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
    import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core';
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default ({
        mixins: [clickMixin],
        props: ['show', 'newcurrency', 'type', 'setup'],
        components: {
            Loading
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                currency: this.newcurrency,
                loading: false,
                render: 0,
                imageSrc: '',
                arabic: '',
                english: '',
                stepsVm: {
                    companyId: '',
                    step1: false,
                    step2: false,
                    step3: false,
                    step4: false,
                },
                renderImg: 0
            }
        },
        validations() {
           return{
             currency:
            {
                name:
                {
                    maxLength: maxLength(50)
                },
                nameArabic:
                {
                    required: requiredIf(function () {
                            return !this.currency.name;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.name == '' || x.name == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(50)
                },
                sign:
                {
                    maxLength: maxLength(10)
                },
                arabicSign:
                {
                     required: requiredIf(function () {
                            return !this.currency.sign;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.sign == '' || x.sign == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(10)
                }
            }
           }
        },
        methods: {
            removeImage: function () {
                this.imageSrc = '';
                this.currency.image = '';
                this.renderImg++;

            },
            uploadImage: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = this.$refs.imgupload.files;

                var fileData = new FormData();

                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }

                this.imageSrc = URL.createObjectURL(this.$refs.imgupload.files[0]);

                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.currency.image = response.data;
                        }
                    },
                        function () {
                            root.loading = false;
                            root.$swal({
                                title: root.$t('AddCurrency.Error'),
                                text: root.$t('AddCurrency.SomethingWrong'),
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            close: function () {
                this.$emit('close');
            },
            SaveCurrency: function () {
                var root = this;
               this.loading = true;
                var url = '/Product/SaveCurrency';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .post(url, root.currency, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {
                                
                                root.$swal({
                                    text: root.$t('AddCurrency.Saved'),
                                    title: root.$t('AddCurrency.SavedSuccessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 800,
                                    timerProgressBar: true,
                                });
                                root.close();
                                if (root.setup) {
                                    root.$emit('CurrencySave', true);
                                    localStorage.setItem('currency', response.data.currency.sign);
                                }
                            }
                            else {
                                root.$swal({
                                    title: root.$t('Updated'),
                                    text: root.$t('UpdateSuccessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 800,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }
                        }
                        else {
                            root.$swal({
                                title: root.$t('Error'),
                                text: root.$t('NameAlreadyExist'),
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                        }
                    });
            }
        },
        mounted: function () {

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.$route.query.data != undefined) {
                this.currency = this.$route.query.data;
            }
        }
    })

</script>