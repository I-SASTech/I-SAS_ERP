<template>
    <modal show="show" :modalLarge="true" v-if=" isValid('CanAddColor') || isValid('CanEditColor') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('AddProductGroup.UpdateProductGroup') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddProductGroup.AddProductGroup') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-bind:key="render" class="form-group col-sm-4">
                        <label>{{ $t('AddColors.Code') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.color.code.$error}">
                            <input readonly class="form-control" v-model="v$.color.code.$model" />
                            <span v-if="v$.color.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddProductGroup.Name'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.color.name.$model" />
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-4 " v-bind:class="{'has-danger' : v$.color.nameArabic.$error}">
                        <label class="text  font-weight-bolder">  {{$arabicLanguage($t('AddProductGroup.NameArabic'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="v$.color.nameArabic.$model" type="text" />
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.color.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddColors.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.color.description.$model" type="text" />
                        <span v-if="v$.color.description.$error" class="error">{{ $t('AddColors.descriptionLength') }}</span>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="color.status">
                            <label for="inlineCheckbox1"> {{ $t('AddColors.Active') }} </label>
                        </div>
                    </div>

                    <div class="form-group has-label col-sm-12">
                        <product-dropdown @update:modelValues="addProduct" width="100%" />

                        <table class="table mt-3">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th width="5%">#</th>
                                    <th width="30%">
                                        {{ $t('AddProductGroup.Name')  }}
                                    </th>
                                    <th width="20%">
                                        {{ $t('AddProductGroup.CategoryName') }}
                                    </th>

                                    <th class="text-end" width="20%">
                                        {{ $t('AddProductGroup.SalePrice') }}
                                    </th>
                                    <th class="text-end" width="20%">
                                        {{ $t('AddProductGroup.PurchasePrice') }}
                                    </th>
                                    <th width="5%"></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(color,index) in color.productList" v-bind:key="index">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditColor(color.id)">{{color.name}}</a>
                                        </strong>
                                    </td>

                                    <td>{{color.categoryName}}</td>
                                    <td class="text-end">{{color.salePrice}}</td>
                                    <td class="text-end">{{color.purchasePrice}}</td>
                                    <td class="text-end">
                                        <a href="javascript:void(0);" @click="removeProduct(color.productId)"><i class="las la-trash-alt text-secondary font-16"></i></a>

                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.color.$invalid" v-if="type!='Edit' && isValid('CanAddColor')">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveColor" v-bind:disabled="v$.color.$invalid" v-if="type=='Edit' && isValid('CanEditColor')">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'newColor', 'type'],
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                color: this.newColor,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
            }
        },
        validations() {
            return {
                color: {
                    name: {
                        maxLength: maxLength(250)
                    },
                    nameArabic: {
                        required: requiredIf(function () {
                            return !this.color.name;
                        }),
                        maxLength: maxLength(250)
                    },
                    code: {
                        required,
                        maxLength: maxLength(30)
                    },
                    description: {
                        maxLength: maxLength(500)
                    }
                }
                }
        },
        methods: {
            addProduct: function (productId, newProduct) {
                
                var prod = this.color.productList.find((x) => x.id == productId);

                if (prod == undefined) {
                    this.color.productList.push({
                        productId: productId,
                        name: newProduct.englishName,
                        salePrice: newProduct.salePrice,
                        purchasePrice: newProduct.purchasePrices,
                        categoryName: newProduct.categoryNameEn,
                    });
                }                
            },

            removeProduct: function (id) {
                this.color.productList = this.color.productList.filter((prod) => {
                    return prod.productId != id;
                });
            },

            close: function () {
                this.$emit('close');
            },

            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/ProductGroupAutoGenerateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.color.code = response.data;
                        root.render++;
                    }
                });
            },

            SaveColor: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Product/SaveProductGroup', this.color, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {

                                root.$swal({
                                    title: root.$t('AddColors.SavedSuccessfully'),
                                    text: root.$t('AddColors.Saved'),
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
                                    title: root.$t('AddColors.SavedSuccessfully'),
                                    text: root.$t('AddColors.UpdateSucessfully'),
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
                                title: root.$t('AddColors.Error'),
                                text: root.$t('AddColors.YourColorNameAlreadyExist'),
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
                                title: root.$t('AddColors.SomethingWrong'),
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
            if (this.color.id == '00000000-0000-0000-0000-000000000000' || this.color.id == undefined || this.color.id == '')
                this.GetAutoCodeGenerator();

        }
    }
</script>
