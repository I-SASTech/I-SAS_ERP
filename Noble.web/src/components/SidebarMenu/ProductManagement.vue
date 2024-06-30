<template>
    <div v-if="isValid('CanViewProduct')|| isValid('CanViewCategory')|| isValid('CanViewSubCategory')|| isValid('CanViewBrand')|| isValid('CanViewOrigin')|| isValid('CanViewSize')|| isValid('CanViewColor')|| isValid('CanViewUnit') " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="setup_sidebar">
            <div class="setup_sidebar_wrapper" >
                <!--<div class="setup_menu" v-if="isValid('CanViewProduct') " v-on:click="GotoPage('/ProductMaster')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Product.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Product') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.CreateProductOrganizeitems') }}
                        </p>
                    </div>
                </div>-->

                <div class="setup_menu" v-if="isValid('CanViewCategory')" v-on:click="GotoPage('/category')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Category.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Category') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.CategoryDescription') }}
                        </p>
                    </div>
                </div> <div class="setup_menu" v-if="isValid('CanViewSubCategory')" v-on:click="GotoPage('/subcategory')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/SubCategory.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Sub-Category') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.Sub-CategoryDescription') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-if="isValid('CanViewBrand')" v-on:click="GotoPage('/brand')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/brand.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Brand') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.BrandDescription') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-if="isValid('CanViewOrigin')" v-on:click="GotoPage('/origin')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">

                            <img src="prod mgmt/Origin.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Origin') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.OriginDescription') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-if="isValid('CanViewSize')" v-on:click="GotoPage('/size')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Size.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Size') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.SizeDescription') }}
                        </p>
                    </div>
                </div> 
                <div class="setup_menu" v-if="isValid('CanViewColor')" v-on:click="GotoPage('/color')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/Color.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Color') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.ColorDescription') }}
                        </p>
                    </div>
                </div>
                
                <div class="setup_menu" v-if="isValid('CanViewUnit')" v-on:click="GotoPage('/unit')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/unit.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('ProductManagement.Unit') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('ProductManagement.UnitDescription') }}
                        </p>
                    </div>
                </div>
                
                <div class="setup_menu" v-if="isValid('CanViewWarrantyType')" v-on:click="GotoPage('/WarrantyType')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/unit.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('WarrantyType.WarrantyType') }}
                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('WarrantyType.WarrantyType') }}
                        </p>
                    </div>
                </div>

                <div class="setup_menu" v-on:click="GotoPage('/PriceLabeling')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/unit.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('PriceLabeling.PriceLabeling') }}

                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('PriceLabeling.PriceLabeling') }}
                        </p>
                    </div>
                </div>
                <div class="setup_menu" v-on:click="GotoPage('/PriceRecord')">
                    <div class="setup_menu_icon">
                        <div class="setup_icon_wrapper">
                            <img src="prod mgmt/unit.svg">
                        </div>
                    </div>
                    <div class="setup_menu_link">
                        <div class="setup_menu_titile">
                            {{ $t('PriceRecord.PriceRecord') }}

                        </div>
                        <p class="setup_menu_desc">
                            {{ $t('PriceRecord.PriceRecord') }}
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
                                <img src="Item Setup.svg" />
                            </div>
                            <div class="col-lg-10 pt-3">
                                <h5 class="page_title  pt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" style="color:white">{{ $t('ProductManagement.ProductManagement') }}</h5>
                                <nav aria-label="breadcrumb" style="color:white !important">
                                    <ol class="breadcrumb" style="color:white !important">
                                        <li class="breadcrumb-item"><router-link :to="'/StartScreen'"><a href="javascript:void(0)" style="color:white !important"> {{ $t('ProductManagement.Home') }}</a></router-link></li>

                                        <li class="breadcrumb-item active" style="color:white !important" aria-current="page"> {{ $t('ProductManagement.InventoryItems') }}</li>
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
                            <h5 class="HeadingOfEmpty">{{ $t('ProductManagement.EmptyForms') }}</h5>
                            <p>{{ $t('ProductManagement.Selectformtheleft') }}</p>
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
