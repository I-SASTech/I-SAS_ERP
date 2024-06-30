<template>

    <modal :show="show" v-if=" isValid('CanAddPaymentOption') || isValid('CanEditPaymentOption') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{
                        $t('AddPaymentOptions.UpdatePaymentOptions')
                    }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{
                        $t('AddPaymentOptions.AddPaymentOptions')
                    }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-sm-12 form-group "
                         v-if="english == 'true'">
                        <label>
                            {{$englishLanguage($t('AddPaymentOptions.NameEn'))   }}:<span class="text-danger">
                                *
                            </span>
                        </label>
                        <input class="form-control" v-model="v$.payment.name.$model" />
                        <span v-if="v$.payment.name.$error" class="error text-danger">
                            <span v-if="!v$.payment.name.required">{{ $t('AddPaymentOptions.NameRequired') }}</span>
                            <span v-if="!v$.payment.name.maxLength">{{ $t('AddPaymentOptions.NameLength') }}</span>
                        </span>
                    </div>
                    <div class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.payment.nameArabic.$error } && ($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'"
                        v-if="isOtherLang()">
                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddPaymentOptions.NameAr'))   }}:
                            <span class="text-danger"> *</span></label>
                        <input class="form-control "
                               v-model="v$.payment.nameArabic.$model" type="text" />
                        <span v-if="v$.payment.nameArabic.$error" class="error">
                            <span v-if="!v$.payment.nameArabic.required">
                               {{$t('AddPaymentOptions.NameRequired') }}
                            </span>
                            <span v-if="!v$.payment.nameArabic.maxLength">
                                {{ $t('AddPaymentOptions.NameLength') }}

                            </span>
                        </span>
                    </div>
                    <div class="col-sm-12 form-group ">
                        <div :key="renderImg">
                            <div class="input-group mb-3" v-if="!((imageSrc == '' && payment.image != '') || (imageSrc != '' && payment.image == '') || (imageSrc != '' && payment.image != ''))">
                                <input ref="imgupload" type="file" class="form-control" id="inputGroupFile02" @change="uploadImage()" accept="image/*"
                                       name="image">
                            </div>

                            <div class="text-right " v-if="imageSrc != ''">
                                <img v-if="imageSrc != ''" class="float-right" :src="imageSrc" width="100" />
                            </div>
                            <div v-else class="text-right ">
                                <span v-if="payment.image!=null">
                                    <productimage v-bind:path="payment.image" />
                                </span>
                            </div>
                        </div>

                    </div>
                    <div class="col-sm-12 form-group " v-if="imageSrc != '' || payment.image != ''">
                        <div class="text-right">
                            <button class="btn btn-danger  btn-sm"
                                    v-on:click="removeImage()">
                                {{ $t('AddPaymentOptions.Remove') }}
                            </button>
                        </div>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="payment.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddPaymentOptions.Active') }} </label>
                        </div>
                    </div>


                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePaymentOptions"
                        v-bind:disabled="v$.payment.$invalid" v-if="type != 'Edit' && isValid('CanAddPaymentOption')">
                    {{
                            $t('AddPaymentOptions.btnSave')
                    }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePaymentOptions"
                        v-bind:disabled="v$.payment.$invalid" v-if="type == 'Edit' && isValid('CanEditPaymentOption')">
                    {{
                            $t('AddPaymentOptions.btnUpdate')
                    }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                        $t('AddPaymentOptions.btnClear')
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
    
    export default ({
        mixins: [clickMixin],
        props: ['show', 'newpayment', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                payment: this.newpayment,
                loading: false,
                render: 0,
                imageSrc: '',
                arabic: '',
                english: '',
                renderImg: 0
            }
        },
        validations() {
            return {
                payment:
                {
                    name:
                    {
                        maxLength: maxLength(50)
                    },
                    nameArabic:
                    {
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        required: requiredIf(function () {
                            return !this.payment.name;
                        }),
                        maxLength: maxLength(50)
                    }
                }
                }
        },
        methods: {
            removeImage: function () {
                this.imageSrc = '';
                this.payment.image = '';
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

                            root.payment.image = response.data;
                        }
                    },
                        function () {
                            root.loading = false;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },

            close: function () {
                this.$emit('close');
            },
            SavePaymentOptions: function () {
                this.loading = true;
                var root = this;
                
                var url = '/Product/SavePaymentOptions?image=' + root.payment.image;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post(url, root.payment, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: "Your Payment Option " + response.data.paymentOptions.name + " has been created!",
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                                root.$emit('RefreshList', true);
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    text: "Your Payment Option " + response.data.paymentOptions.name + " has been updated!",
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                                root.$emit('RefreshList', true);

                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your Payment Option  Already Exist!' : 'خيار الدفع الخاص بك موجود بالفعل!',
                                type: 'error',
                                icon: 'success',
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
                this.paymentOptions = this.$route.query.data;
            }
        }
    })

</script>