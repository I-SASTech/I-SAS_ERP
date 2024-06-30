<template>

<modal :show="show" v-if="isValid('CanAddDeduction') || isValid('CanEditDeduction')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{
                        $t('AddDeduction.UpdateDeduction')
                }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{ $t('AddDeduction.AddDeduction') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.deduction.code.$error}">
                                    <label class="text  font-weight-bolder"> {{ $t('AddDeduction.Code') }}:<span class="text-danger"> *</span></label>
                                    <input disabled class="form-control"  v-model="v$.deduction.code.$model" type="text" />
                                </div>

                                <div v-if="english=='true'" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.deduction.nameInPayslip.$error}">
                                    <label class="text  font-weight-bolder"> {{ $t('AddDeduction.NameInPayslipEnglish')}}: <span class="text-danger"> *</span></label>
                                    <input class="form-control"  v-model="v$.deduction.nameInPayslip.$model" type="text" />
                                </div>

                                <div v-if="isOtherLang()" class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : v$.deduction.nameInPayslipArabic.$error}">
                                    <label class="text  font-weight-bolder">{{ $t('AddDeduction.NameInPayslipArabic')}}: <span class="text-danger"> *</span></label>
                                    <input class="form-control "  v-model="v$.deduction.nameInPayslipArabic.$model" type="text" />
                                </div>

                                <div class="form-group has-label col-sm-12 ">
                                    <label>{{ $t('AddDeduction.CalculateAmount') }} :<span class="text-danger"> *</span></label>
                                    <multiselect :options="calculateAmountOptions" v-model="deduction.amountType" :show-labels="false" v-bind:placeholder="$t('AddDeduction.SelectType')" >
                                    </multiselect>
                                </div>
                                
                                <div class="form-group has-label col-sm-12 ">
                                    <label>{{ $t('AddDeduction.DeductionTaxPlan') }} :<span class="text-danger"> *</span></label>
                                    <multiselect :options="taxOptions" v-model="deduction.taxPlan" :show-labels="false" v-bind:placeholder="$t('AddDeduction.SelectType')" >
                                    </multiselect>
                                </div>
                                

                    <div class="form-group has-label col-sm-12">
                        <label class="text  font-weight-bolder">{{ $t('AddDeduction.AmountPercentage') }}: <span
                                class="text-danger"> *</span></label>
                        <div class="input-group">
                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                <i v-if="deduction.amountType == '% of Salary' || deduction.amountType == '٪ من الراتب'"
                                    class="fa fa-percent"></i>
                                <i v-else>{{ currency }}</i></button>
                            <input v-model="deduction.amount" type="text" class="form-control"
                                @focus="$event.target.select()" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>
                       
                    </div>
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="deduction.active">
                            <label for="inlineCheckbox1"> {{ $t('AddDeduction.Status') }} </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDeduction"
                    v-bind:disabled="v$.deduction.$invalid" v-if="type != 'Edit' && isValid('CanAddDeduction')">{{
                            $t('AddDeduction.Save')
                    }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveDeduction"
                    v-bind:disabled="v$.deduction.$invalid" v-if="type == 'Edit' && isValid('CanEditDeduction')">{{
                            $t('AddDeduction.Update')
                    }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('AddDeduction.Cancel')
                }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import { required,maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
     import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    import Multiselect from 'vue-multiselect'

    export default {
        mixins: [clickMixin],
        props: ['show', 'newDeduction', 'type'],
        components: {
            Loading,
            Multiselect
        },
          setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                deduction: this.newDeduction,
                currency: '',
                arabic: '',
                english: '',
                loading: false,
                taxOptions:[],
                calculateAmountOptions:[],
            }
        },
        validations() {
          return{
              deduction: {
                nameInPayslip: {
                    maxLength: maxLength(100)
                },
                code: {
                    maxLength: maxLength(30)
                },
                amount: {
                    required
                },
                amountType: {
                    required
                },
                taxPlan: {
                    required
                },
                nameInPayslipArabic: {
                     required: requiredIf(function () {
                            return !this.deduction.nameInPayslip;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.nameInPayslip == '' || x.nameInPayslip == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(100)
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
                this.$https.get('/Payroll/DeductionAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.deduction.code = response.data;
                    }
                });
            },
            SaveDeduction: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                

                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (this.deduction.amountType == '% of Salary') {
                        this.deduction.amountType = 1;
                    }
                    else if (this.deduction.amountType == 'Fixed') {
                        this.deduction.amountType = 2;
                    }

                    if (this.deduction.taxPlan == 'Non Taxable') {
                        this.deduction.taxPlan = 2;
                    }
                    else if (this.deduction.taxPlan == 'Taxable') {
                        this.deduction.taxPlan = 1;
                    }
                }
                else {
                    if (this.deduction.amountType == '٪ من الراتب') {
                        this.deduction.amountType = 1;
                    }
                    else if (this.deduction.amountType == 'مثبت') {
                        this.deduction.amountType = 2;
                    }
                    if (this.deduction.taxPlan == 'غير خاضعة للضريبة') {
                        this.deduction.taxPlan = 2;
                    }
                    else if (this.deduction.taxPlan == 'خاضع للضريبة') {
                        this.deduction.taxPlan = 1;
                    }
                }

                this.$https.post('/Payroll/SaveDeductionInformation', this.deduction, { headers: { "Authorization": `Bearer ${token}` } })
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
                                text: "Your Brand Name  Already Exist!",
                                type: 'error',
                                icon: 'error',
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
            }
        },
        created: function () {
            if (this.deduction.id == '00000000-0000-0000-0000-000000000000' || this.deduction.id == undefined || this.deduction.id == '')
                this.GetAutoCodeGenerator();
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.currency = localStorage.getItem('currency');

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.calculateAmountOptions = ['% of Salary', 'Fixed'];
                this.taxOptions = ['Taxable', 'Non Taxable'];
            }
            else {
                this.calculateAmountOptions = ['٪ من الراتب', 'مثبت'];
                this.taxOptions = ['خاضع للضريبة', 'غير خاضعة للضريبة'];
            }

        }
    }
</script>
