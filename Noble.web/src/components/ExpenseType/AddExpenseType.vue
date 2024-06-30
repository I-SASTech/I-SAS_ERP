<template>
    <modal :show="show">
        <div style="margin-bottom:0px" class="card" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddExpenseType.UpdateExpenseType') }} </h5>

                        </div>
                        <div class="modal-header" v-else>
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddExpenseType.AddExpenseType') }} </h5>
                        </div>
                        <div>
                            <div class="card-body ">
                                <div class="row ">
                                    <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
                                        <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddExpenseType.ExpenseTypeName'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="process.expenseTypeName" type="text" />
                                    </div>
                                    <div v-if="arabic=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.process.expenseTypeArabic.$error}">
                                        <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddExpenseType.ExpenseTypeName'))  }}: <span class="text-danger"> *</span></label>
                                        <input class="form-control arabicLanguage " v-model="v$.process.expenseTypeArabic.$model" type="text" />
                                        <span v-if="v$.process.expenseTypeArabic.$error" class="error">
                                            <span v-if="!v$.process.expenseTypeArabic.required"> {{ $t('AddExpenseType.NameRequired') }}</span>
                                        </span>
                                    </div>
                                    <div class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> Expense Account: <span class="text-danger"> *</span></label>
                                        <accountdropdown v-model="process.accountId" :formName="'Expense'"  />

                                    </div>
                                    <div v-if="arabic=='true'" class="form-group has-label col-sm-12 " >
                                        <label class="text  font-weight-bolder"> Description:</label>
                                        <textarea class="form-control"  v-model="process.description" type="text" />
                                      
                                    </div>
                                    <div class="form-group col-md-4">
                                        <div class="checkbox form-check-inline mx-2">
                                            <input type="checkbox" id="inlineCheckbox1" v-model="process.status">
                                            <label for="inlineCheckbox1"> {{ $t('AddExpenseType.Active') }}</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer" v-if="type=='Edit' && isValid('CanEditExpenseType')">
                                <button type="button" class="btn btn-soft-primary btn-sm " v-on:click="SaveProcess" v-bind:disabled="v$.process.$invalid"> {{ $t('AddExpenseType.btnUpdate') }}</button>
                                <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddExpenseType.btnClear') }}</button>
                            </div>
                            <div class="modal-footer " v-if="type!='Edit' && isValid('CanAddExpenseType')">
                                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveProcess" v-bind:disabled="v$.process.$invalid"> {{ $t('AddExpenseType.btnSave') }}</button>
                                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddExpenseType.btnClear') }}</button>
                            </div>
                        </div>
                        <div v-else>
                            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import { requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'newProcess', 'type'],
        mixins: [clickMixin],
        components: {
            Loading
        },
         setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                process: this.newProcess,
                arabic: '',
                english: '',
                render: 0,
                randerAccount: 0,

                loading: false,
                processList: []
            }
        },
        validations() {
        return{
                process: {
                expenseTypeArabic: {
                    required: requiredIf(function () {
                            return !this.process.expenseTypeName;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.expenseTypeName == '' || x.expenseTypeName == null)
                    //         return true;
                    //     return false;
                    // }),
                }
            }
        }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveProcess: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.post('/Accounting/SaveExpenseType', this.process, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {

                            if (root.type != "Edit") {

                                root.$swal({
                                    title: "Saved!",
                                    text: "Saved Successfully!",
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
                                    title: "Saved!",
                                    text: "Update Sucessfully!",
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();
                            }
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
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
