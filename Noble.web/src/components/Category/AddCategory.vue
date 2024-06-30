<template>
    <modal :show="show" v-if=" isValid('CanAddCategory') || isValid('CanEditCategory') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">{{ $t('AddCategory.UpdateCategory') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddCategory.AddCategory') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-bind:key="rendered" class="form-group col-sm-12">
                        <label>{{ $t('AddCategory.Code') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.category.code.$error}">
                            <input readonly class="form-control" v-model="v$.category.code.$model" />
                            <span v-if="v$.category.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddCategory.CategoryName'))   }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.category.name.$model" />
                        <span v-if="v$.category.name.$error" class="error">
                            <span v-if="!v$.category.name.required"> {{ $t('AddCategory.NameRequired') }}</span>
                            <span v-if="!v$.category.name.maxLength">{{ $t('AddCategory.NameLength') }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.category.nameArabic.$error}">
                        <label class="text  font-weight-bolder">{{$arabicLanguage($t('AddCategory.CategoryNameAr'))   }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="v$.category.nameArabic.$model" type="text" />
                        <span v-if="v$.category.nameArabic.$error" class="error">
                            <span v-if="!v$.category.nameArabic.required"> {{ $t('AddCategory.NameRequired') }}</span>
                            <span v-if="!v$.category.nameArabic.maxLength">{{ $t('AddCategory.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.category.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddCategory.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.category.description.$model" type="text" />
                        <span v-if="v$.category.description.$error" class="error">{{ $t('AddCategory.descriptionLength150') }}</span>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="category.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddCategory.Active') }} </label>
                        </div>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox2" v-model="category.isService">
                            <label for="inlineCheckbox2">{{ $t('AddCategory.Service') }}  </label>
                        </div>
                    </div>

                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox3" v-model="category.isReturn">
                            <label for="inlineCheckbox3">{{ $t('AddCategory.ReturnDays') }}  </label>
                        </div>
                    </div>
                    <div v-if="category.isReturn" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">{{ $t('AddCategory.ReturnDays') }}:</label>
                        <input class="form-control" v-model="category.returnDays" @click="$event.target.select()" type="text" />

                    </div>


                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCategory" v-bind:disabled="v$.category.$invalid" v-if="type!='Edit' && isValid('CanAddCategory')">{{ $t('AddCategory.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveCategory" v-bind:disabled="v$.category.$invalid" v-if="type=='Edit' && isValid('CanEditCategory')">{{ $t('AddCategory.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddCategory.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default ({
        mixins: [clickMixin],
        name: 'AddCategory',
        props: ['show', 'newcategory', 'type'],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {

            return {
                value: '',
                loading: false,
                rendered: 0,
                arabic: '',
                english: '',
                category: this.newcategory
            }
        },
        validations() {
            return {
                category:
                {
                    code: { required },
                    name: {
                        maxLength: maxLength(250)
                    },
                    nameArabic: {
                        required: requiredIf(function () {
                            return !this.category.name;
                        }),
                        // required: requiredIf((x) => {
                        //     if (x.name == '' || x.name == null)
                        //         return true;
                        //     return false;
                        // }),
                        maxLength: maxLength(250)
                    },
                    description: {
                        maxLength: maxLength(500)
                    },
                    purchaseAccountId: {},
                    cOGSAccountId: {},
                    inventoryAccountId: {},
                    incomeAccountId: {},
                    saleAccountId: {}


                }
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveCategory: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.category.isReturn == false) {
                    this.category.returnDays = 0;
                }
                this.$https
                    .post('/Product/SaveCategoryInformation', this.category, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data.isSuccess == true) {
                            if (root.type != "Edit") {
                                root.$swal({
                                    title: root.$t('AddCategory.Saved'),
                                    text: root.$t('AddCategory.SavedSuccessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.$emit('IsSave');
                                root.close();
                            }
                            else {


                                root.$swal({
                                    title: root.$t('AddCategory.Update'),
                                    text: root.$t('AddCategory.UpdateSucessfully'),
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                                root.$emit('IsSave');
                            }
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: root.$t('AddCategory.SomethingWrong'),
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
            AutoIncrementCode: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/Product/CategoryAutoGenerateCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.category.code = response.data;
                            root.rendered++
                        }
                    });
            },
        }
        ,
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if (this.category.id == "00000000-0000-0000-0000-000000000000") {
                this.AutoIncrementCode();
            }
        }
    })

</script>