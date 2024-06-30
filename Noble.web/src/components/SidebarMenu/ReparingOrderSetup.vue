<template>
    <div v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">


                <div class="setup_menu" v-on:click="GotoPage('/ReparingOrderType','WarrantyCategory')" style="height:65px!important" v-if="isValid('CanViewWarrantyCategory')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/CityAndTown.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ReparingOrder.WarrantyCategory') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ReparingOrder.WarrantyCategory') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ReparingOrderType','UpsDescription')" style="height:65px!important" v-if="isValid('CanViewDescription')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ReparingOrder.UpsDescription') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ReparingOrder.UpsDescription') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ReparingOrderType','Problem')" style="height:65px!important" v-if="isValid('CanViewProblem')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ReparingOrder.Problem') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ReparingOrder.Problem') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/ReparingOrderType','AcessoryIncluded')" style="height:65px!important" v-if="isValid('CanViewAccessory')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Region.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ReparingOrder.AcessoryIncluded') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ReparingOrder.AcessoryIncluded') }}
                        </p>
                    </div>
                </div>
              
                


            </div>
        </div>
        <div class="setup_main_panel" v-bind:class="$i18n.locale == 'en' ? '' : 'setup_main_panel_ar'">

            <div class="col-md-12 col-lg-12 ">
                <div class="card img mt-2 mb-2" style="background-color: #3178F6;" v-bind:class="$i18n.locale == 'en' ? 'imageLeft' : 'imageRight'">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-2 pt-2 ">
                                <img src="Global business.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">  {{ $t('ReparingOrder.ReparingOrderSetup') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('GeographicalSetup.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page">{{ $t('ReparingOrder.ReparingOrder') }}</li>
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
