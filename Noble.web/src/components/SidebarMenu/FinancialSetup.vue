<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">
                <div class="setup_menu" v-if="isValid('CanViewCurrency')" v-on:click="GotoPage('/currency')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/currency.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.SetupCurrency') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.SetupCurrencyDescription') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-if="isValid('CanViewBank')" v-on:click="GotoPage('/Bank')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/bank.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.RegisterBank') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.RegisterBankDescription') }}
                        </p>
                    </div>
                </div>
                <!--<div class="setup_menu" v-on:click="GotoPage('/ChequeAndGuranteeDashboard')" v-if="isValid('CanViewCheque')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/bank.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ChequeBook.ChequeDashboard') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ChequeBook.ChequeDashboard') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ChequeAndGurantee')" v-if="isValid('CanViewCheque')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/bank.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ChequeBook.Cheques&Guarrntees') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ChequeBook.Cheques&Guarrntees') }}
                        </p>
                    </div>
                </div>-->
                <div class="setup_menu" v-if="isValid('CanAddMonthlyCost')" v-on:click="GotoPage('/monthlycost')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/monthly op cost.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.SetupMonthlyCost') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.SetupMonthlyCostDescription') }}
                        </p>
                    </div>
                </div>
                <!--<div class="setup_menu" v-if="isValid('CanViewCOA')" v-on:click="GotoPage('/coa')" style="height:90px !important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/chart of account coa.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.SetupCOA') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.SetupCOADescription') }}
                        </p>
                    </div>
                </div>-->
                <div class="setup_menu" v-if="isValid('CanViewPaymentOption')" v-on:click="GotoPage('/PaymentOptions')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/payment options.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.PaymentOptions') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.PaymentOptionsDescription') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-if="isValid('CanViewDenomination')" v-on:click="GotoPage('/DenominationSetup')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/denomination setup.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.DenominationSetup') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.DenominationSetup') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-if="isValid('CanViewVatRate')" v-on:click="GotoPage('/taxrate')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/vat rate.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.VATSetup') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.VATSetup') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-if="isValid('CanViewExpenseType')" v-on:click="GotoPage('/ExpenseType')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/expense type.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.ExpenseType') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.ExpenseType') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-if="isValid('CanViewFinancialYear')" v-on:click="GotoPage('/FinancialYear')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="/financial year.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('FinancialSetup.FinancialYear') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('FinancialSetup.FinancialYear') }}
                        </p>
                    </div>
                </div>



            </div>
        </div>
        <div class="setup_main_panel" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'setup_main_panel_ar'">

            <div class="col-md-12 col-lg-12 ">
                <div class="card img mt-2 mb-2" style="background-color: #3178F6;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'imageLeft' : 'imageRight'">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-2 pt-2 ">
                                <img src="Financial Setup.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">{{ $t('FinancialSetup.FinancialSetup') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('FinancialSetup.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page">{{ $t('FinancialSetup.FinancialSetup') }}</li>
                                    </ol>
                                </nav>
                            </div>
                        </div>

                    </div>
                </div>
                <div v-if="path == 'FinancialSetup' ">
                    <div class="row">
                        <div class="col-12"  style="text-align:center;width:250px; height:250px;margin-top:141px">
                            <img src="Empty form.svg" />
                            <h5 class="HeadingOfEmpty">{{ $t('FinancialSetup.EmptyForms') }}</h5>
                            <p>{{ $t('FinancialSetup.Selectformtheleft') }}</p>
                        </div>
                    </div>
                </div>
                <div v-else>
                    <router-view></router-view>

                </div>
            </div>

        </div>
    </div>
    <!--<div v-else> <acessdenied></acessdenied></div>-->
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
               
                path: 'FinancialSetup'

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
