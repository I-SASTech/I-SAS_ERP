<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-if="isValid('CanPrintRackBarcode') || isValid('CanPrintItemBarcode')">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">


                <div class="setup_menu" v-on:click="GotoPage('/MultiBarcodePrinting')" style="height:85px !important" v-if="isValid('CanPrintItemBarcode')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/Stock report.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('BarcodeSetup.ItemBarcode') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('BarcodeSetup.ItemBarcodeDesc') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/RackBarCodeCreate')" style="height:85px !important" v-if="isValid('CanPrintRackBarcode')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="Reports/Stock report.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('BarcodeSetup.RacksBarcode') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('BarcodeSetup.RackBarcodeDesc') }}
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
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">{{ $t('BarcodeSetup.BarcodeSetup') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('BarcodeSetup.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page">{{ $t('BarcodeSetup.BarcodeSetup') }}</li>
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
                            <h5 class="HeadingOfEmpty">{{ $t('BarcodeSetup.EmptyForms') }}</h5>
                            <p>{{ $t('BarcodeSetup.Selectformtheleft') }}</p>
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
    }
</script>
<style scoped>
    .img {
        height: 160px;
        background-size: contain !important;
        background-repeat: no-repeat !important;
    }
</style>
