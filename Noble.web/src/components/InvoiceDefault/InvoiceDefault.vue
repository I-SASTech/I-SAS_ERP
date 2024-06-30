<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('InvoiceDefault.InvoiceDefault') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('InvoiceDefault.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('InvoiceDefault.InvoiceDefault') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-12">

                <div class="card-body">
                    <div class="row">
                        
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2 " >
                                    <input type="checkbox" id="isSalePrice" v-on:change="GetValueOfSale()" v-model="invoiceDefault.isSalePrice">
                                    <label for="isSalePrice">{{ $t('InvoiceDefault.AreyousureyouWantSalePrice') }}:</label>
                                </div><br>
                                
                            <div class="checkbox form-check-inline mx-2 " >
                                    <input type="checkbox" id="isCustomerPrice" v-on:change="GetValueOfCustomer()" v-model="invoiceDefault.isCustomerPrice" >
                                    <label for="isCustomerPrice">{{ $t('InvoiceDefault.AreyousureyouwantCustomerPrice') }}</label>
                                </div>
                            
                           

                        </div>
                        <div class="col-lg-6">
                            <div class="checkbox form-check-inline mx-2 " >
                                    <input type="checkbox" id="isSalePriceLabel" v-on:change="GetValueOfSaleLabel()" v-model="invoiceDefault.isSalePriceLabel" >
                                    <label for="isSalePriceLabel">{{ $t('InvoiceDefault.AreyousureyouWantPriceLabelingOnSale') }}:</label>
                                </div><br>
                               
                                <div class="checkbox form-check-inline mx-2 " >
                                    <input type="checkbox" id="isCustomerPriceLabel" v-on:change="GetValueOfCustomerLabel()" v-model="invoiceDefault.isCustomerPriceLabel">
                                    <label for="isCustomerPriceLabel">{{ $t('InvoiceDefault.AreyousureyouwantPriceLabelingonCustomer') }}</label>
                                </div>
                            
                           

                        </div>
                      
                    </div>
                </div>
                <div class="card-footer">
                    <button type="button" class="btn btn-outline-primary ms-5 me-2"  v-on:click="SaveSetting"><i class="far fa-save" ></i>{{ $t('InvoiceDefault.Save') }} </button>
                    <button type="button" class="btn btn-outline-danger" v-on:click="GotoPage('/StartScreen')">{{ $t('InvoiceDefault.Cancel') }}</button>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>
 <script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default ({
        name: "InvoiceDefault",
        components: {
            Loading
        },
        mixins: [clickMixin],
        data: function () {
            return {
                loading: false,
                randerValue:0,
                invoiceDefault: {
                    id: '00000000-0000-0000-0000-000000000000',
                    isSalePrice: false,
                    isSalePriceLabel: false,
                    isCustomerPrice: false,
                    isCustomerPriceLabel: false,
                 
                    
                }
            }
        },
       
        methods: {
            GetValueOfSale: function () {
                 
                this.invoiceDefault.isSalePriceLabel = false;

                // this.invoiceDefault.isSalePrice1!=this.invoiceDefault.isSalePrice;
                // this.randerValue++;             
            },
            GetValueOfSaleLabel: function () {
                 
                // this.invoiceDefault.isSalePrice!=this.invoiceDefault.isSalePrice1;
                  
                this.invoiceDefault.isSalePrice = false;
                this.randerValue++;
            },
            GetValueOfCustomer: function () {
                //  
                // this.invoiceDefault.isCustomerPrice1!=this.invoiceDefault.isCustomerPrice;
                // this.randerValue++; 
                this.invoiceDefault.isCustomerPriceLabel = false;
            },
            GetValueOfCustomerLabel: function () {
                //  
                // this.invoiceDefault.isCustomerPrice!=this.invoiceDefault.isCustomerPrice1;
                // this.randerValue++; 
                this.invoiceDefault.isCustomerPrice = false;
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            SaveSetting: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.post('/Company/SaveInvoiceDefault', this.invoiceDefault, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.$swal({
                                title: "Saved!",
                                text: "Saved Successfully!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.GetData();
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },

            GetData: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/InvoiceDefaultDetails', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                         

                        if (response.data != null) {
                            root.invoiceDefault = response.data;

                            localStorage.setItem('IsSalePrice', root.invoiceDefault.isSalePrice);
                            localStorage.setItem('IsSalePriceLabel', root.invoiceDefault.isSalePriceLabel);
                            localStorage.setItem('IsCustomerPrice', root.invoiceDefault.isCustomerPrice);
                            localStorage.setItem('IsCustomerPriceLabel', root.invoiceDefault.isCustomerPriceLabel);
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false)
            },
        },
        created: function () {
            this.GetData()

        },
        mounted: function () {

        }
    })
</script> 