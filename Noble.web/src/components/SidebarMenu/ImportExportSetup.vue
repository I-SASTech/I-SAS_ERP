<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">


                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','StuffingLocation')" v-if="isValid('CanViewStuffingLocation')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/CityAndTown.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.StuffingLocation') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.StuffingLocation') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','PortOfLoading')" v-if="isValid('CanViewPartOfLoading')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.PortOfLoading') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.PortOfLoading') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','PortOfDestination')" v-if="isValid('CanViewPartOfDestination')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.PortOfDestination') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.PortOfDestination') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','OrderType')" v-if="isValid('CanViewOrderType')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.OrderType') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.OrderType') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','Service')" v-if="isValid('CanViewService')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.Service') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.Service') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','Incoterms')" v-if="isValid('CanViewIncoterms')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.Incoterms') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.Incoterms') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','Commodity')" v-if="isValid('CanViewCommodity')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.Commodity') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.Commodity') }}
                        </p>
                    </div>
                </div>
               
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','Carrier')" v-if="isValid('CanViewCarrier')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.Carrier') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.Carrier') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','ExportEXW')" v-if="isValid('CanViewExportExw')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.ExportEXW') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.ExportEXW') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','ImportFOB')" v-if="isValid('CanViewImportFob')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.ImportFOB') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.ImportFOB') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ImportExport','QuantityContainer')" v-if="isValid('CanViewQuantityContainer')" style="height:65px!important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('importExport.QuantityContainer') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('importExport.QuantityContainer') }}
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
                                <img src="Global business.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" style="color:white">  {{ $t('importExport.importExport') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('GeographicalSetup.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page">{{ $t('importExport.importExport') }}</li>
</ol>
                                </nav>
                            </div>
                        </div>

                    </div>
                </div>
                <div v-if="path == 'GeographicalSetup' ">
                    <div class="row">
                        <div class="col-12" style="text-align:center;width:250px; height:250px;margin-top:141px">
                            <img src="Empty form.svg" />
                            <h5 class="HeadingOfEmpty">{{ $t('GeographicalSetup.EmptyForms') }}</h5>
                            <p>{{ $t('GeographicalSetup.Selectformtheleft') }}</p>
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
            GotoPage: function (link, formName) {
                this.path = link;

                this.$router.push({
                    path: link,
                    query: {
                        formName: formName,
                    }

                });

               
            },

        },
        created: function () {
            this.path = this.$route.name;
            this.$emit('update:modelValue', this.$route.name);
        },
    }
</script>
<style scoped>
    .img {
        height: 160px;
        background-size: contain !important;
        background-repeat: no-repeat !important;
    }
</style>
