<template>
    <modal :show="show" v-if=" isValid('CanAddAllowance') || isValid('CanEditAllowance') ">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">
                    {{
                        $t('AddAllowance.UpdateAllowance')
                    }}
                </h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>
                    {{ $t('AddAllowance.AddAllowance') }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div :key="render" class="form-group has-label col-sm-12 "
                         v-bind:class="{ 'has-danger': v$.allowance.code.$error }">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddAllowance.Code') }}:<span class="text-danger"> *</span>
                        </label>
                        <input disabled class="form-control" v-model="v$.allowance.code.$model" type="text" />
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddAllowance.AllowanceType') }}:<span class="text-danger"> *</span>
                        </label>
                        <allowanceTypeDropdown v-model="allowance.allowanceTypeId" :modelValue="allowance.allowanceTypeId">
                        </allowanceTypeDropdown>
                    </div>
                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddAllowance.CalculateAmount') }} :<span class="text-danger"> *</span>
                        </label>
                        <multiselect v-model="allowance.amountType" :options="calculateAmountOptions"
                                     :show-labels="false" v-bind:placeholder="$t('AddAllowance.SelectType')">
                        </multiselect>
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddAllowance.AllowanceTaxPlan') }} :<span class="text-danger"> *</span>
                        </label>
                        <multiselect v-model="allowance.taxPlan" :options="taxOptions" :show-labels="false"
                                     v-bind:placeholder="$t('AddAllowance.SelectType')">
                        </multiselect>
                    </div>

                    <div class="form-group has-label col-sm-12">
                        <label class="text  font-weight-bolder">
                            {{ $t('AddAllowance.AmountPercentage') }}: <span class="text-danger"> *</span>
                        </label>
                        <div class="input-group">
                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                <i v-if="allowance.amountType == '% of Salary' || allowance.amountType == '٪ من الراتب'"
                                   class="fa fa-percent"></i>
                                <i v-else>{{ currency }}</i>
                            </button>
                            <input v-model="allowance.amount" type="text" class="form-control"
                                   @focus="$event.target.select()" aria-label="Example text with button addon"
                                   aria-describedby="button-addon1">
                        </div>

                    </div>
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="allowance.isActive">
                            <label for="inlineCheckbox1"> {{ $t('AddAllowance.Status') }} </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAllowance"
                        v-bind:disabled="v$.allowance.$invalid" v-if="type != 'Edit' && isValid('CanAddAllowance')">
                    {{
                            $t('AddAllowance.Save')
                    }}
                </button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveAllowance"
                        v-bind:disabled="v$.allowance.$invalid" v-if="type == 'Edit' && isValid('CanEditAllowance')">
                    {{
                            $t('AddAllowance.Update')
                    }}
                </button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">
                    {{
                        $t('AddAllowance.Cancel')
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
    import { maxLength, required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect'
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
        props: ['show', 'newallowance', 'type'],
        components: {
            Multiselect,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                currency: '',
                calculateAmountOptions: [],
                taxOptions: [],

                arabic: '',
                english: '',
                render: 0,
                loading: false,
                allowance: {},
            }
        },
        validations() {
            return {
                allowance: {
                    allowanceTypeId: {
                        required
                    },
                    amountType: {
                        required
                    },
                    taxPlan: {
                        required
                    },
                    amount: {
                        required
                    },

                    code: {

                        maxLength: maxLength(30)
                    },

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
                this.$https.get('/Payroll/AllowanceAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.allowance.code = response.data;
                        root.render++;
                    }
                });
            },
            SaveAllowance: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    if (this.allowance.amountType == '% of Salary') {
                        this.allowance.amountType = 1;
                    }
                    else if (this.allowance.amountType == 'Fixed') {
                        this.allowance.amountType = 2;
                    }
                    if (this.allowance.taxPlan == 'Non Taxable') {
                        this.allowance.taxPlan = 2;
                    }
                    else if (this.allowance.taxPlan == 'Taxable') {
                        this.allowance.taxPlan = 1;
                    }

                }
                else {
                    if (this.allowance.amountType == '٪ من الراتب') {
                        this.allowance.amountType = 1;
                    }
                    else if (this.allowance.amountType == 'مثبت') {
                        this.allowance.amountType = 2;
                    }
                    if (this.allowance.taxPlan == 'غير خاضعة للضريبة') {
                        this.allowance.taxPlan = 2;
                    }
                    else if (this.allowance.taxPlan == 'خاضع للضريبة') {
                        this.allowance.taxPlan = 1;
                    }

                }
                this.$https.post('/Payroll/SaveAllowanceInformation', this.allowance, { headers: { "Authorization": `Bearer ${token}` } })
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
                                text: "Your Allowance Name  Already Exist!",
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
            this.allowance = this.newallowance;
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.calculateAmountOptions = ['% of Salary', 'Fixed'];
                this.taxOptions = ['Taxable', 'Non Taxable'];
            }
            else {
                this.calculateAmountOptions = ['٪ من الراتب', 'مثبت'];
                this.taxOptions = ['خاضع للضريبة', 'غير خاضعة للضريبة'];
            }
            this.currency = localStorage.getItem('currency');
            if (this.allowance.id == '00000000-0000-0000-0000-000000000000' || this.allowance.id == undefined || this.allowance.id == '')
                this.GetAutoCodeGenerator();

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                if (this.allowance.amountType == 1) {
                    this.allowance.amountType = '% of Salary';
                }
                else if (this.allowance.amountType == 2) {
                    this.allowance.amountType = 'Fixed';
                }
                if (this.allowance.taxPlan == 2) {
                    this.allowance.taxPlan = 'Non Taxable';
                }
                else if (this.allowance.taxPlan == 1) {
                    this.allowance.taxPlan = 'Taxable';
                }

            }
            else {

                if (this.allowance.amountType == 1) {
                    this.allowance.amountType = '٪ من الراتب';
                }
                else if (this.allowance.amountType == 2) {
                    this.allowance.amountType = 'مثبت';
                }
                if (this.allowance.taxPlan == 2) {
                    this.allowance.taxPlan = 'غير خاضعة للضريبة';
                }
                else if (this.allowance.taxPlan == 1) {
                    this.allowance.taxPlan = 'خاضع للضريبة';
                }
            }

        }
    }
</script>

