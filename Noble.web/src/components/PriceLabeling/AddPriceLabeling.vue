<template>
    <modal :show="show" >
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddPriceLabeling.UpdateProductPriceLabeling') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else >{{ $t('AddPriceLabeling.AddProductPriceLabeling') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-bind:key="render" class="form-group col-sm-12">
                        <label>{{ $t('AddPriceLabeling.Code') }}:<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.priceLabeling.code.$error}">
                            <input readonly class="form-control" v-model="v$.priceLabeling.code.$model" />
                            <span v-if="v$.priceLabeling.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('AddPriceLabeling.PriceLabelingNameEnglish')}}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.priceLabeling.name.$model" />
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.priceLabeling.nameArabic.$error}">
                        <label class="text  font-weight-bolder">{{ $t('AddPriceLabeling.PriceLabelingNameArabic') }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="v$.priceLabeling.nameArabic.$model" type="text" />

                    </div>
                    <!-- <div  class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{ $t('AddPriceLabeling.Price')}}:<span class="text-danger"> *</span> </label>
                        <decimal-to-fixed v-bind:salePriceCheck="false" v-model="v$.priceLabeling.price.$model" :textAlignLeft="true" />
                    </div> -->

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.priceLabeling.description.$error}">
                        <label class="text  font-weight-bolder">  {{ $t('AddPriceLabeling.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.priceLabeling.description.$model" type="text" />

                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="priceLabeling.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddPriceLabeling.Status') }} </label>
                        </div>
                    </div>




                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePriceLabeling" v-bind:disabled="v$.priceLabeling.$invalid" v-if="type!='Edit' ">{{ $t('AddPriceLabeling.Save') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SavePriceLabeling" v-bind:disabled="v$.priceLabeling.$invalid" v-if="type=='Edit' ">{{ $t('AddPriceLabeling.Update') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddPriceLabeling.Cancel') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default {
        mixins: [clickMixin],
        props: ['show', 'newPriceLabel', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                priceLabeling: '',
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
            return {
                priceLabeling: {
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
                            return !this.priceLabeling.name;
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
                this.$https.get('/Product/PriceLabelingCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        root.priceLabeling.code = response.data;
                        root.render++;
                    }
                });
            },
            SavePriceLabeling: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SavePriceLabeling', this.priceLabeling, { headers: { "Authorization": `Bearer ${token}` } })
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
                            root.loading = false
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
                            root.loading = false
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Your PriceLabeling Name Already Exist!' : 'اسم تسمية السعر الخاص بك موجود بالفعل!',
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.loading = false
                    }
                    })
                    .catch(error => {
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
                    .finally(() => root.loading = false);
            }
        },
        created: function () {
            
            this.priceLabeling = this.newPriceLabel;
            console.log(this.newPriceLabel)
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.priceLabeling.id == '00000000-0000-0000-0000-000000000000' || this.priceLabeling.id == undefined || this.priceLabeling.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
