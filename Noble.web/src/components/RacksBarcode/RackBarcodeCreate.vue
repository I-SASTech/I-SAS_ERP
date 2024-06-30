<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title"> {{ $t('RackBarcodeCreate.RacksBarcode') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('RackBarcodeCreate.Home') }}</a></li>
                                    <li class="breadcrumb-item active"> {{ $t('RackBarcodeCreate.RacksBarcode') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row  ">
                <div class="col-md-8 col-sm-12 mb-2 mb-lg-0 ">
                    <div class="card p-3  pb-3">
                        <div class="row mt-2 ">
                            <div class="col-lg-6 col-6 mb-2 mb-lg-0">
                                <label>{{ $t('RackBarcodeCreate.SelectCategory') }} :<span class="text-danger"> *</span></label>
                                <div v-bind:class="{'has-danger' : v$.racksBarcode.categoryId.$error}">
                                    <categorydropdown v-model="v$.racksBarcode.categoryId.$model" @update:modelValue="getProductList"></categorydropdown>
                                    <span v-if="v$.racksBarcode.categoryId.$error" class="error text-danger">
                                        <span v-if="!v$.racksBarcode.categoryId.required">{{$t('RackBarcodeCreate.CategoryRequired')}}</span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-lg-6 col-6 mb-2 mb-lg-0">
                                <label class="text  font-weight-bolder"> {{ $t('RackBarcodeCreate.PrintPerProduct') }}: </label>
                                <input class="form-control" v-model="racksBarcode.totalPrintPerProduct" type="number" />

                            </div>
                            <div class="col-sm-12 mt-3">
                                <label>{{ $t('RackBarcodeCreate.PleaseSelectProduct') }} :<span class="text-danger"> *</span></label>
                                <div>
                                    <productMultiSelectDropdown v-model="racksBarcode.productIdList" :key="render" :modelValue="racksBarcode.productIdList" :categoryId="racksBarcode.categoryId" :isRequest="isRequest"></productMultiSelectDropdown>

                                </div>
                            </div>
                        </div>

                        <div v-if="racksBarcode.productIdList.length > 0" class=" float-right mt-3">


                            <button type="button" class="btn btn-primary  " v-on:click="RackBarCodePrintBtn" v-bind:disabled="v$.racksBarcode.$invalid"> {{ $t('RackBarcodeCreate.Print') }}</button>
                            <router-link :to="'/StartScreen'"><a href="javascript:void(0)" class="btn btn-outline-danger mx-2"> {{ $t('RackBarcodeCreate.Close') }}</a></router-link>

                        </div>
                        <div v-else class=" float-left mt-3 ">
                            <button type="button" class="btn btn-primary  " v-on:click="RackBarCodePrintBtn" disabled> {{ $t('RackBarcodeCreate.Print') }}</button>
                            <router-link :to="'/StartScreen'"><a href="javascript:void(0)" class="btn btn-outline-danger mx-2 "> {{ $t('RackBarcodeCreate.Close') }}</a></router-link>

                        </div>
                    </div>

                </div>
            </div>

        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>




</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        name: 'RacksBarcodePrint',
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {

            return {
                loading: false,
                arabic: '',
                english: '',
                render: 0,
                options: [],
                isRequest: false,
                racksBarcode: {
                    categoryId: '',
                    productIdList: [],
                    totalPrintPerProduct: 1
                },
                totalProductList: [],
                printRender: 0,
                image: ''
            }
        },
        validations() {
            return {
                racksBarcode:
                {

                    categoryId: {
                        required
                    }
                }
            }
        },
        methods: {

            RackBarCodePrintBtn: function () {
                var root = this;
                root.totalProductList = [];

                root.racksBarcode.productIdList.forEach(function (x) {
                    for (let i = 0; i < root.racksBarcode.totalPrintPerProduct; i++) {

                        root.totalProductList.push(x);
                    }

                })

                //alert(root.totalProductList);
                this.printRender++;

            },
            getProductList: function () {

                this.isRequest = true;
                this.render++;

            },
            SaveSubCategory: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveSubCategoryInformation', this.subCategory, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {
                                root.$swal({
                                    icon: 'success',
                                    title: 'Saved Successfully!',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    icon: 'success',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
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
                                icon: 'error',
                                text: "Your Sub Category Already Exist!",
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
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
            },

        },
        created() {
            this.$emit('update:modelValue', this.$route.name);
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

        }
    })

</script>