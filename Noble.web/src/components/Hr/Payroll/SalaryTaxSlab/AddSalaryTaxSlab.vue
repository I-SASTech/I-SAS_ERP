<template>
    <div class="row" v-if="isValid('CanAddSalaryTaxSlab') || isValid('CanEditSalaryTaxSlab')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 v-if="salaryTaxSlab.id === '00000000-0000-0000-0000-000000000000'"
                                    class="page-title">{{
                                            $t('AddSalaryTaxSlab.AddSalaryTaxSlab')
                                    }}</h4>
                                <h4 v-else class="page-title">{{ $t('AddSalaryTaxSlab.UpdateSalaryTaxSlab') }}</h4>
                            </div>
                            <div class="col-auto align-self-center">

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr class="hr-dashed hr-menu mt-0" />

            <div class="row mb-5">

                <div class="col-lg-6">
                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSalaryTaxSlab.FromDate')
                            }} </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="salaryTaxSlab.fromDate" />
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">

                    <div class="row form-group">
                        <label class="col-form-label col-lg-4">
                            <span class="tooltip-container text-dashed-underline "> {{ $t('AddSalaryTaxSlab.ToDate') }}
                            </span><span class="text-danger"> *</span>
                        </label>
                        <div class="inline-fields col-lg-8">
                            <datepicker v-model="salaryTaxSlab.toDate" />
                        </div>
                    </div>

                </div>

                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-12">
                            <table class="table mb-0">
                                <thead class="thead-light table-hover">
                                    <tr>
                                        <th style="width:5%;">#</th>

                                        <th style="width:20%;text-align:center;">
                                            {{ $t('AddSalaryTaxSlab.IncomeSlabFrom') }}
                                        </th>
                                        <th style="width:20%;text-align:center;">
                                            {{ $t('AddSalaryTaxSlab.IncomeSlabTo') }}
                                        </th>
                                        <th style="width:20%;text-align:center;">
                                            {{ $t('AddSalaryTaxSlab.FixTax') }}
                                        </th>
                                        <th style="width:20%;text-align:center;">
                                            {{ $t('AddSalaryTaxSlab.Rate') }}
                                        </th>
                                        <th style="width:5%;" class="text-center">
                                            {{ $t('AddSalaryTaxSlab.Action') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(slab, index) in salaryTaxSlab.salaryTaxSlabList" v-bind:key="slab.id">
                                        <td>
                                            {{ index + 1 }}
                                        </td>

                                        <td>
                                            <input type="number" v-model="slab.incomeFrom"
                                                @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>
                                        <td>
                                            <input type="number" v-model="slab.incomeTo" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>
                                        <td>
                                            <input type="number" v-model="slab.fixedTax" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>
                                        <td>
                                            <input type="number" v-model="slab.rate" @focus="$event.target.select()"
                                                class="form-control input-border text-center tableHoverOn" />
                                        </td>
                                        <td class="text-center">
                                            <a href="javascript:void(0);" @click="RemoveTaxSlab(index)"><i class="las la-trash-alt text-secondary font-16"></i></a>
                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7" class="border-top-0">
                                            <button id="but_add" class="btn btn-success btn-sm" v-on:click="AddTaxSlab()">+
                                               {{ $t('AddSalaryTaxSlab.AddTaxSlab') }} </button>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>


                        </div>
                    </div>
                </div>


            </div>
            <div class="row">


                <div class="col-lg-12 invoice-btn-fixed-bottom ">

                    <div class="button-items">
                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate"
                            v-if="salaryTaxSlab.id == '00000000-0000-0000-0000-000000000000' && isValid('CanAddSalaryTaxSlab')"
                            v-bind:disabled="v$.salaryTaxSlab.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddSalaryTaxSlab.Save') }}
                        </button>

                        <button class="btn btn-outline-primary  mr-2" v-on:click="SaveSalaryTemplate"
                            v-if="salaryTaxSlab.id != '00000000-0000-0000-0000-000000000000' && isValid('CanEditSalaryTaxSlab')"
                            v-bind:disabled="v$.salaryTaxSlab.$invalid">
                            <i class="far fa-save"></i> {{ $t('AddSalaryTaxSlab.Update') }}
                        </button>

                        <button class="btn btn-danger  mr-2" v-on:click="Close()">
                            {{ $t('AddSalaryTaxSlab.Cancel') }}
                        </button>
                    </div>
                </div>
            </div>
        </div>

        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
// import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
import moment from 'moment'
export default ({
    mixins: [clickMixin],
setup() {
            return { v$: useVuelidate() }
        },
         components: {
            Loading
        },
    data: function () {
        return {
             loading: false,
            currency: '',
            arabic: '',
            english: '',
            salaryTaxSlab: {
                id: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                // incomeFrom: 0,
                // incomeTo: 0,
                // fixedTax: 0,
                // rate: 0,
                salaryTaxSlabList: [{
                    incomeFrom: 0, 
                    incomeTo: 0, 
                    fixedTax: 0,
                     rate: 0  
                }],
            },
            language: 'Nothing',
        }
    },
    validations() {
      return{
          salaryTaxSlab:
        {
            fromDate: {
                required
            },
            toDate: {
                required
            },

        },
      }

    },
    computed: {
        // isValueValid: function () {
        //     if (this.salaryTaxSlab.incomeFrom >= 0 && this.salaryTaxSlab.incomeTo >= 0 && this.salaryTaxSlab.fixedTax >= 0 && this.salaryTaxSlab.rate >= 0) {
        //         return false;
        //     }

        //     return true;
        // },

    },

    methods: {
        AddTaxSlab: function () {
            // this.salaryTaxSlab.incomeFrom = this.salaryTaxSlab.incomeFrom == '' ? 0 : this.salaryTaxSlab.incomeFrom;
            // this.salaryTaxSlab.incomeTo = this.salaryTaxSlab.incomeTo == '' ? 0 : this.salaryTaxSlab.incomeTo;
            // this.salaryTaxSlab.fixedTax = this.salaryTaxSlab.fixedTax == '' ? 0 : this.salaryTaxSlab.fixedTax;
            // this.salaryTaxSlab.rate = this.salaryTaxSlab.rate == '' ? 0 : this.salaryTaxSlab.rate;

            this.salaryTaxSlab.salaryTaxSlabList.push({ incomeFrom: 0, incomeTo: 0, fixedTax: 0, rate: 0 });
            // this.salaryTaxSlab.incomeFrom = 0;
            // this.salaryTaxSlab.incomeTo = 0;
            // this.salaryTaxSlab.fixedTax = 0;
            // this.salaryTaxSlab.rate = 0;
        },

        roundValua: function (x) {
            return parseFloat(x).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")
        },

        RemoveTaxSlab: function (index) {
            this.salaryTaxSlab.salaryTaxSlabList.splice(index, 1);
        },


        languageChange: function (lan) {

            if (this.language == lan) {
                if (this.salaryTemplate.id == '00000000-0000-0000-0000-000000000000') {

                    var getLocale = this.$i18n.locale;
                    this.language = getLocale;

                    this.$router.go('/AddSalaryTaxSlab');
                }
                else {
                    this.$swal({
                        title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text:(this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                        type: 'error',
                        confirmButtonClass: "btn btn-danger",
                        icon: 'error',
                        timer: 4000,
                        timerProgressBar: true,
                    });
                }
            }
        },
        Close: function () {
            this.$router.push('/SalaryTaxSlab');
        },
        SaveSalaryTemplate: function () {

            this.loading = true;
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            root.$https
                .post('/Payroll/SaveSalaryTaxSlab', root.salaryTaxSlab, { headers: { "Authorization": `Bearer ${token}` } })
                .then(response => {

                    if (response.data.isSuccess) {
                        root.loading = false

                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        }).then(function (ok) {
                            if (ok != null) {
                                root.$router.push('/SalaryTaxSlab');
                            }
                        });
                    }

                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message.isAddUpdate,
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 1500,
                            timerProgressBar: true,
                        });
                    }

                })
                .catch(error => {
                    console.log(error)
                    this.$swal.fire(
                        {
                            icon: 'error',
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                            text: error,
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,

                        });

                    this.loading = false
                })
                .finally(() => this.loading = false)
        }
    },
    created: function () {
        var root =  this;

if(root.$route.query.Add == 'false')
{
    root.$route.query.data = this.$store.isGetEdit;
}
        this.$emit('update:modelValue', this.$route.name);

        if (this.$route.query.data != undefined) {
            var data = this.$route.query.data;
            this.salaryTaxSlab.id = data.id;
            this.salaryTaxSlab.fromDate = data.fromDate;
            this.salaryTaxSlab.toDate = data.toDate;
            this.salaryTaxSlab.salaryTaxSlabList = data.salaryTaxSlabList;
        }
        else {
            this.salaryTaxSlab.fromDate = moment().startOf('year').format('llll');
            this.salaryTaxSlab.toDate = moment().endOf('year').format('llll');
        }
    },
    mounted: function () {
        this.currency = localStorage.getItem('currency');
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.language = this.$i18n.locale;
    }
})
</script>
<style scoped>
.checkBoxHeight {
    width: 20px;
    height: 30px;
}

.input-group-append .input-group-text,
.input-group-prepend .input-group-text {
    background-color: #e3ebf1;
    border: 1px solid #e3ebf1;
    color: #000000;
}

.input-group .form-control {
    border-left: 1px solid #e3ebf1;
}

.input-group .form-control:focus {
    border-left: 1px solid #3178F6;
}

.input-group-text {
    border-radius: 0;
}
</style>