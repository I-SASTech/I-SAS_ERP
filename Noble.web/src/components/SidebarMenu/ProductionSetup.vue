<template>
    <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper">
               

          
                <div class="setup_menu" v-on:click="GotoPage('/SampleRequest')" style="height:85px !important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/CityAndTown.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            Sample Request
                        </div>
                        <p class="setup_menu_desc">
                            Sample Request
                        </p>
                    </div>
                </div>
               
                <div class="setup_menu" v-on:click="GotoPage('/Process')" style="height:85px !important">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/CityAndTown.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            Process
                        </div>
                        <p class="setup_menu_desc">
                            Process
                        </p>
                    </div>
                </div>
               



            </div>
        </div>
        <div class="setup_main_panel" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'setup_main_panel_ar'">

            <div class="col-md-12 col-lg-12 ">
               
              
                <div>
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
    }
</script>
<style scoped>
    .img {
        height: 160px;
        background-size: contain !important;
        background-repeat: no-repeat !important;
    }
</style>
