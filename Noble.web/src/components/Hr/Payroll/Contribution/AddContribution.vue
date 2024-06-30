<template>
    <modal :show="show" v-if="isValid('CanAddContribution') || isValid('CanEditContribution')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type == 'Edit'">{{
                        $t('AddContribution.UpdateContribution')
                }}</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>{{
                        $t('AddContribution.AddContribution')
                }}
                </h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.contribution.code.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddContribution.Code') }}:<span
                                class="text-danger"> *</span></label>
                        <input disabled class="form-control" v-model="v$.contribution.code.$model" type="text" />
                    </div>

                    <div v-if="english == 'true'" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.contribution.nameInPayslip.$error }">
                        <label class="text  font-weight-bolder"> {{ $t('AddContribution.NameInPayslipEnglish') }}: <span
                                class="text-danger"> *</span></label>
                        <input class="form-control" v-model="v$.contribution.nameInPayslip.$model" type="text" />
                    </div>

                    <div v-if="isOtherLang()" class="form-group has-label col-sm-12 "
                        v-bind:class="{ 'has-danger': v$.contribution.nameInPayslipArabic.$error }">
                        <label class="text  font-weight-bolder">{{ $t('AddContribution.NameInPayslipArabic') }}: <span
                                class="text-danger"> *</span></label>
                        <input class="form-control " v-model="v$.contribution.nameInPayslipArabic.$model" type="text" />
                    </div>

                    <div class="form-group has-label col-sm-12 ">
                        <label>{{ $t('AddContribution.CalculateAmount') }} :<span class="text-danger"> *</span></label>
                        <multiselect :options="calculateAmountOptions" v-model="contribution.amountType"
                            :show-labels="false" v-bind:placeholder="$t('AddContribution.SelectType')"
                            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                        </multiselect>
                    </div>



                    <div class="form-group has-label col-sm-12">
                        <label class="text  font-weight-bolder">{{ $t('AddContribution.AmountPercentage') }}: <span
                                class="text-danger"> *</span></label>
                        <div class="input-group">
                            <button class="btn btn-secondary" type="button" id="button-addon1">
                                <i v-if="contribution.amountType == '% of Salary' || contribution.amountType == '٪ من الراتب'"
                                    class="fa fa-percent"></i>
                                <i v-else>{{ currency }}</i></button>
                            <input v-model="contribution.amount" type="text" class="form-control"
                                @focus="$event.target.select()" aria-label="Example text with button addon"
                                aria-describedby="button-addon1">
                        </div>

                    </div>
                    <div class="form-group col-md-4">
                        <div class="checkbox form-check-inline mx-2">
                            <input type="checkbox" id="inlineCheckbox1" v-model="contribution.active">
                            <label for="inlineCheckbox1"> {{ $t('AddContribution.Status') }} </label>
                        </div>
                    </div>


                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveContribution"
                    v-bind:disabled="v$.contribution.$invalid"
                    v-if="type != 'Edit' && isValid('CanAddContribution')">{{
                            $t('AddContribution.Save')
                    }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveContribution"
                    v-bind:disabled="v$.contribution.$invalid"
                    v-if="type == 'Edit' && isValid('CanEditContribution')">{{
                            $t('AddContribution.Update')
                    }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{
                        $t('AddContribution.Cancel')
                }}</button>
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


import Multiselect from 'vue-multiselect'

export default {
    mixins: [clickMixin],
    props: ['show', 'newContribution', 'type'],
    components: {
        Loading,
        Multiselect
    },
      setup() {
            return { v$: useVuelidate() }
        },

    data: function () {
        return {
            contribution: this.newContribution,
            currency: '',
            arabic: '',
            english: '',
            loading: false,
            calculateAmountOptions: [],
        }
    },
    validations() {
       return{
         contribution: {
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

            nameInPayslipArabic: {
                required: requiredIf(function () {
                            return !this.contribution.nameInPayslip;
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
            this.$https.get('/Payroll/ContributionAutoGenerateNo', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                if (response.data != null) {
                    root.contribution.code = response.data;
                }
            });
        },
        SaveContribution: function () {
            var root = this;
            this.loading = true;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }


            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                if (this.contribution.amountType == '% of Salary') {
                    this.contribution.amountType = 1;
                }
                else if (this.contribution.amountType == 'Fixed') {
                    this.contribution.amountType = 2;
                }


            }
            else {
                if (this.contribution.amountType == '٪ من الراتب') {
                    this.contribution.amountType = 1;
                }
                else if (this.contribution.amountType == 'مثبت') {
                    this.contribution.amountType = 2;
                }

            }

            this.$https.post('/Payroll/SaveContributionInformation', this.contribution, { headers: { "Authorization": `Bearer ${token}` } })
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
        if (this.contribution.id == '00000000-0000-0000-0000-000000000000' || this.contribution.id == undefined || this.contribution.id == '')
            this.GetAutoCodeGenerator();
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.currency = localStorage.getItem('currency');

        if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
            this.calculateAmountOptions = ['% of Salary', 'Fixed'];
        }
        else {
            this.calculateAmountOptions = ['٪ من الراتب', 'مثبت'];
        }

    }
}
</script>