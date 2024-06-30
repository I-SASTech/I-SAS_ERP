<template>
    <div v-if=" isValid('CanViewAllowanceType') || isValid('CanViewAllowance') || isValid('CanViewDeduction') || isValid('CanViewContribution') || isValid('CanViewPayRollSchedule') || isValid('CanViewSaleryTemplate') || isValid('CanViewEmployeeSalary') || isValid('CanViewLoanPayment') || isValid('CanViewSalaryTaxSlab')" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">
                <div class="setup_menu" v-on:click="GotoPage('/AllowanceType')" v-if=" isValid('CanViewAllowanceType')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>

                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('AllowanceType.AllowanceType') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('AllowanceType.AllowanceType') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/Allowance')"  v-if=" isValid('CanViewAllowance')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>

                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('Allowance.Allowance') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('Allowance.Allowance') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/Deduction')"  v-if=" isValid('CanViewDeduction')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('Deduction.Deduction') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('Deduction.Deduction') }}
                        </p>
                    </div>

                </div>
                <div class="setup_menu" v-on:click="GotoPage('/Contribution')"  v-if=" isValid('CanViewContribution')"> 
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('Contribution.Contribution') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('Contribution.Contribution') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-on:click="GotoPage('/PayrollSchedule')"  v-if=" isValid('CanViewPayRollSchedule')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('PayrollSchedule.PayrollSchedule') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('PayrollSchedule.PayrollSchedule') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-on:click="GotoPage('/SalaryTemplate')"  v-if=" isValid('CanViewSaleryTemplate')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('SalaryTemplate.SalaryTemplate') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('SalaryTemplate.SalaryTemplate') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-on:click="GotoPage('/EmployeeSalary')"  v-if=" isValid('CanViewEmployeeSalary')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('EmployeeSalary.EmployeeSalary') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('EmployeeSalary.EmployeeSalary') }}
                        </p>
                    </div>
                </div> 
                <div class="setup_menu" v-on:click="GotoPage('/LoanPayment')"  v-if=" isValid('CanViewLoanPayment')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('LoanPayment.LoanPayment') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('LoanPayment.LoanPayment') }}
                        </p>
                    </div>
                </div>
                
                <div class="setup_menu" v-on:click="GotoPage('/SalaryTaxSlab')"  v-if=" isValid('CanViewSalaryTaxSlab')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('SalaryTaxSlab.SalaryTaxSlab') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('SalaryTaxSlab.SalaryTaxSlab') }}
                        </p>
                    </div>
                </div>
                
                <!--<div class="setup_menu" v-on:click="GotoPage('/RunPayroll')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            Run Payroll
                        </div>
                        <p class="setup_menu_desc">
                            Run Payroll
                        </p>
                    </div>
                </div>-->

            </div>
        </div>

        <div class="setup_main_panel" v-bind:class="$i18n.locale == 'en'  ? '' : 'setup_main_panel_ar'">
          
            <div class="col-md-12 col-lg-12 ">
                <div class="card img mt-2 mb-2" style="background-color: #3178F6;" v-bind:class="$i18n.locale == 'en'  ? 'imageLeft' : 'imageRight'">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-2 pt-2 ">
                                <img src="Item Setup.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">{{ $t('Payroll.Payroll') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page"> {{ $t('Payroll.Payroll') }}</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>

                    </div>
                </div>
                <div v-if="path == 'ProductManagement' ">
                    <div class="row">
                        <div class="col-12" style="text-align:center;width:250px; height:250px;margin-top:141px">
                            <img src="Empty form.svg" />
                            <h5 class="HeadingOfEmpty">{{ $t('EmptyForms') }}</h5>
                            <p>{{ $t('Selectformtheleft') }}</p>
                        </div>
                    </div>
                </div>
                <div v-else>
                    <router-view></router-view>

                </div>
            </div>

        </div>
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data() {
            return {

                path: 'ProductManagement',
                isMasterProduct: false
            }
        },
        methods: {
            GotoPage: function (link) {
                this.path = link;
                this.$router.push({
                    path: link,
                });
            },

        },
        created: function () {
            this.path = this.$route.name;
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {

            this.isMasterProduct = localStorage.getItem('IsMasterProductPermission') == 'true' ? true : false

        }
    }


</script>
<style scoped>
    .img {
        height: 160px;
        background-size: contain !important;
       
        background-repeat: no-repeat !important;
        
    }
    
</style>
