<template>
    <div >
        <div class="col-md-12 ml-auto mr-auto m-4">
            <div class="card">
                <div class="card-body">
                    <div class="col-lg-12" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                        <div class="card-header p-0 ">
                            <h5> {{ $t('AddAccountSetup.CompanyAccountsSetup') }}:</h5>
                        </div>
                        <div class="row" v-bind:key="render">
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span> {{ $t('AddAccountSetup.Inventory') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.inventoryAccountId"></accountdropdown>

        </div>
    </div>-->
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span> {{ $t('AddAccountSetup.VATPayable') }}:</span>
                                <div class="form-group">
                                    <accountdropdown v-model="companyAccountSetup.vatPayableAccountId"></accountdropdown>

                                </div>
                            </div>
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.VATReceivable') }}:</span>
                                <div class="form-group">
                                    <accountdropdown v-model="companyAccountSetup.vatReceiableAccountId"></accountdropdown>

                                </div>
                            </div>
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.DiscountPayable') }}:</span>
                                <div class="form-group">
                                    <accountdropdown v-model="companyAccountSetup.discountPayableAccountId"></accountdropdown>

                                </div>
                            </div>
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.DiscountReceivable') }}:</span>
                                <div class="form-group">
                                    <accountdropdown v-model="companyAccountSetup.discountReceivableAccountId"></accountdropdown>

                                </div>
                            </div>
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.Bank') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.bankId"></accountdropdown>

        </div>
    </div>-->
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.Cash') }}:</span>
                                <div class="form-group">
                                    <accountdropdown v-model="companyAccountSetup.cashId"></accountdropdown>

                                </div>
                            </div>
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.TaxMethod') }} :</span>
                                <multiselect :options="[ 'Inclusive', 'Exclusive']" v-model="companyAccountSetup.taxMethod" :show-labels="false" placeholder="Select Type">
                                </multiselect>
                            </div>
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.Sale') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.saleAccountId"></accountdropdown>

        </div>
    </div>-->
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.FreeOfCost') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.freeofCostAccountId"></accountdropdown>

        </div>
    </div>-->
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.StockIn') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.stockInAccountId"></accountdropdown>

        </div>
    </div>-->
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.StockOut') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.stockOutAccountId"></accountdropdown>

        </div>
    </div>-->
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.Bundle') }}:</span>
        <div class="form-group" >
            <accountdropdown v-model="companyAccountSetup.bundleAccountId"></accountdropdown>

        </div>
    </div>-->
                            <!--<div class=" col-lg-4 col-md-4 col-sm-4">
        <span>{{ $t('AddAccountSetup.Promotion') }}:</span>
        <div class="form-group">
            <accountdropdown v-model="companyAccountSetup.promotionAccountId"></accountdropdown>

        </div>
    </div>-->
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.BarcodeType') }}:</span>
                                <div class="form-group">
                                    <barcodeDropdown v-model="companyAccountSetup.barcodeType" :modelValue="companyAccountSetup.barcodeType" v-bind:key="render">
                                    </barcodeDropdown>

                                </div>
                            </div>
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.AddCurrency') }}:</span>
                                <div class="form-group">
                                    <currency-dropdown v-model="companyAccountSetup.currencyId" :modelValue="companyAccountSetup.currencyId" :isCurrency=true v-bind:key="render">
                                    </currency-dropdown>

                                </div>
                            </div>
                            <div class=" col-lg-4 col-md-4 col-sm-4">
                                <span>{{ $t('AddAccountSetup.TaxRate') }}:</span>
                                <div class="form-group">
                                    <taxratedropdown v-model="companyAccountSetup.taxRateId" :modelValue="companyAccountSetup.taxRateId" :isCurrency=true v-bind:key="render">
                                    </taxratedropdown>

                                </div>
                            </div>
                        </div>
                        <div>
                            <div v-if="companyAccountSetup.id == '00000000-0000-0000-0000-000000000000' || companyAccountSetup.id == undefined">
                                <button class="btn btn-reset  float-right mr-2" v-on:click="BackToList()">{{ $t('AddAccountSetup.btnClear') }}</button>
                                <button v-if="isValid('Can Save Company Setup')" class="btn btn-primary  float-right mr-2" v-on:click="SaveCompanyAccountSetup()">{{ $t('AddAccountSetup.btnSave') }}</button>
                            </div>
                            <div v-else>
                                <button class="btn btn-danger  float-right mr-2" v-on:click="BackToList()">{{ $t('AddAccountSetup.btnClear') }}</button>
                                <button v-if="isValid('Can Edit Company Setup')" class="btn btn-primary  float-right mr-2" v-on:click="SaveCompanyAccountSetup()">{{ $t('AddAccountSetup.btnUpdate') }}</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    //import { required } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default ({
        components: {
            Multiselect
        },
        mixins: [clickMixin],
        data: function () {
            return {
                render: 0,
                companyAccountSetup: {
                    id: '00000000-0000-0000-0000-000000000000',
                    inventoryAccountId: '',
                    vatPayableAccountId: '',
                    vatReceiableAccountId: '',
                    saleAccountId: '',
                    discountPayableAccountId: '',
                    discountReceivableAccountId: '',
                    freeofCostAccountId: '',
                    stockInAccountId: '',
                    stockOutAccountId: '',
                    bundleAccountId: '',
                    promotionAccountId: '',
                    cashId: '',
                    bankId: '',
                    barcodeType: '',
                    currencyId: '',
                    taxMethod: '',
                    taxRateId:''
                },
                curreny: ''
            }
        },
        methods: {
            BackToList: function () {
                if (this.$route.query.isProceed != undefined) {
                    this.$router.push('/Setup')
                }
                else {


                    this.$router.push('/StartScreen')
                }

            },
            SaveCompanyAccountSetup: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                localStorage.setItem('currency', this.companyAccountSetup.currencyId);
                localStorage.setItem('taxMethod', this.companyAccountSetup.taxMethod);
                this.$https.post('/Company/SaveCompanyAccountSetup', this.companyAccountSetup, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data == '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                            text: "Data Saved Successfully",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        });
                       
                        if (root.$route.query.isProceed != undefined) {
                            root.$router.push({
                                path: '/Setup',
                                query: {
                                    account: true
                                }
                            });
                        }
                        else {
                            this.$router.push('/StartScreen')
                        }
                    }
                    if (response.data != '00000000-0000-0000-0000-000000000000') {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                            text: "Data Updated Successfully",
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                            confirmButtonClass: "btn btn-success",
                            buttonsStyling: false
                        });
                        if (root.$route.query.isProceed != undefined) {
                            root.$router.push({
                                path: '/Setup',
                                query: {
                                    account: true
                                }
                            });
                        }
                        else {
                            this.$router.push('/StartScreen')
                        }
                    }
                });
            },
            GetLastDetails: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/CompanyAccountSetupDetails', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.companyAccountSetup = response.data;


                        root.render++;
                    }
                });
            }
        },
        mounted: function () {
            this.GetLastDetails();

        }
    })

</script>