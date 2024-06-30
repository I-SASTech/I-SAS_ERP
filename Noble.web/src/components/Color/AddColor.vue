<template>
    <modal show="show"  v-if=" isValid('CanAddColor') || isValid('CanEditColor') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'"> {{ $t('AddColors.UpdateProductColor') }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddColors.AddProductColor') }}</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div v-bind:key="render" class="form-group col-sm-12">
                        <label>{{ $t('AddColors.Code') }} :<span class="text-danger"> *</span></label>
                        <div v-bind:class="{'has-danger' : v$.color.code.$error}">
                            <input readonly class="form-control" v-model="v$.color.code.$model" />
                            <span v-if="v$.color.code.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>
                    <div v-if="english=='true'" class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddColors.ColorName'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control" v-model="v$.color.name.$model" />
                        <span v-if="v$.color.name.$error" class="error">
                            <span v-if="!v$.color.name.required"> {{ $t('AddColors.NameRequired') }}</span>
                            <span v-if="!v$.color.name.maxLength">{{ $t('AddColors.NameRequired') }}</span>
                        </span>
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.color.nameArabic.$error}">
                        <label class="text  font-weight-bolder">  {{$arabicLanguage($t('AddColors.ColorNameAr'))  }}:<span class="text-danger"> *</span> </label>
                        <input class="form-control " v-model="v$.color.nameArabic.$model" type="text" />
                        <span v-if="v$.color.nameArabic.$error" class="error">
                            <span v-if="!v$.color.nameArabic.required"> {{ $t('AddColors.NameRequired') }}</span>
                            <span v-if="!v$.color.nameArabic.maxLength">{{ $t('AddColors.NameLength') }}</span>
                        </span>
                    </div>

                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.color.description.$error}">
                        <label class="text  font-weight-bolder"> {{ $t('AddColors.Description') }}: </label>
                        <textarea rows="3" class="form-control" v-model="v$.color.description.$model" type="text" />
                        <span v-if="v$.color.description.$error" class="error">{{ $t('AddColors.descriptionLength') }}</span>
                    </div>


                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="color.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddColors.Active') }} </label>
                        </div>
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
        props: ['show', 'newcolor', 'type'],
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
                color: this.newcolor,
            }
        },
        validations() {
            return{
                color: {
                name: {
                    maxLength: maxLength(250)
                },
                nameArabic: {
                        required: requiredIf(function () {
                            return !this.color.name;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.name == '' || x.name == null)
                    //         return true;
                    //     return false;
                    // }),
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
            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Product/ColorCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

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
                this.$https.post('/Product/SaveColor', this.color, { headers: { "Authorization": `Bearer ${token}` } })
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
                                title: this.$t('AddColors.SomethingWrong'),
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
