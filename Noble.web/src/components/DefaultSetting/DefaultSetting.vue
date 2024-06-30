<template>
    <div class="row">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('DefaultSettings.DefaultSettings') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('DefaultSettings.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('DefaultSettings.DefaultSettings') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card col-md-6">
                <div class="card-body">
                    <div class="row ">

                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.Sale') }} : <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isSaleCredit" name="contact-sub-type1" id="a499464971" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a499464971">{{ $t('DefaultSettings.Cash') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isSaleCredit" name="contact-sub-type1" id="a9ff8eb352" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a9ff8eb352">{{ $t('DefaultSettings.Credit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.Purchase') }} : <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isPurchaseCredit" name="contact-sub-type2" id="a499464973" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a499464973">{{ $t('DefaultSettings.Cash') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isPurchaseCredit" name="contact-sub-type2" id="a9ff8eb354" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a9ff8eb354">{{ $t('DefaultSettings.Credit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.Customer') }} : <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isCustomeCredit" name="contact-sub-type3" id="a499464975" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a499464975">{{ $t('DefaultSettings.Cash') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isCustomeCredit" name="contact-sub-type3" id="a9ff8eb356" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a9ff8eb356">{{ $t('DefaultSettings.Credit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.Supplier') }} : <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isSupplierCredit" name="contact-sub-type4" id="a499464977" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a499464977">{{ $t('DefaultSettings.Cash') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isSupplierCredit" name="contact-sub-type4" id="a9ff8eb358" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a9ff8eb358">{{ $t('DefaultSettings.Credit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.CustomerPayReceipt') }} : <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isCustomerPayCredit" name="contact-sub-type5" id="a499464979" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a499464979">{{ $t('DefaultSettings.Cash') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isCustomerPayCredit" name="contact-sub-type5" id="a9ff8eb351a" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a9ff8eb351a">{{ $t('DefaultSettings.Credit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.SupllierPayReceipt') }}: <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isSupplierPayCredit" name="contact-sub-type6" id="a499464972a" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a499464972a">{{ $t('DefaultSettings.Cash') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isSupplierPayCredit" name="contact-sub-type6" id="a9ff8eb353a" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a9ff8eb353a">{{ $t('DefaultSettings.Credit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                        <fieldset class="form-group"  v-if="cashCustomer">
                            <div class="row">
                                <div class="col-form-label col-lg-4 pt-0">
                                    <span id="ember694" class="tooltip-container text-dashed-underline ">{{ $t('DefaultSettings.CashCustomer') }}: <span class="text-danger">*</span></span>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isCashCustomer" name="contact-sub-type7" id="a499464972ac" class=" form-check-input" type="radio" v-bind:value="true">
                                        <label class="form-check-label pl-0" for="a499464972ac">{{ $t('DefaultSettings.CashCustomer') }}</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input v-model="defaultSetting.isCashCustomer" name="contact-sub-type7" id="a9ff8eb353ab" class=" form-check-input" type="radio" v-bind:value="false">
                                        <label class="form-check-label pl-0" for="a9ff8eb353ab">{{ $t('DefaultSettings.CashCredit') }}</label>
                                    </div>
                                </div>
                            </div>
                        </fieldset>


                        <div class="col-sm-12 arabicLanguage">
                            <button type="button" class="btn btn-outline-primary me-2" v-on:click="SaveSetting"><i class="far fa-save"></i> {{ $t('DefaultSettings.Save') }}</button>

                            <button type="button" class="btn btn-outline-danger" v-on:click="Close()">{{ $t('DefaultSettings.Cancel') }}</button>
                        </div>
                    </div>
                </div>

            </div>
               <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
    </div>
</template>
<script>
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    
    export default ({
         components: {
            Loading
        },
        data: function () {
            return {
                  loading: false,
                cashCustomer:false,
                defaultSetting: {
                    isSaleCredit: false,
                    isPurchaseCredit: false,
                    isCustomeCredit: false,
                    isCustomerPayCredit: false,
                    isSupplierPayCredit: false,
                    isSupplierCredit: false,
                    isCashCustomer: false,
                }
            }
        },

        methods: {
          
            SaveSetting: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                this.$https
                    .post('/Sale/AddUpdateDefaultSetting', this.defaultSetting, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            localStorage.setItem('IsSaleCredit', root.defaultSetting.isSaleCredit);
                            localStorage.setItem('IsPurchaseCredit', root.defaultSetting.isPurchaseCredit);
                            localStorage.setItem('IsCustomeCredit', root.defaultSetting.isCustomeCredit);
                            localStorage.setItem('IsCustomerPayCredit', root.defaultSetting.isCustomerPayCredit);
                            localStorage.setItem('IsSupplierPayCredit', root.defaultSetting.isSupplierPayCredit);
                            localStorage.setItem('IsSupplierCredit', root.defaultSetting.isSupplierCredit);
                            localStorage.setItem('IsCashCustomer', root.defaultSetting.isCashCustomer);
                            root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
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
                
                this.$https
                    .get('/Sale/GetDefaultSetting', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {

                        if (response.data != null) {
                            root.defaultSetting.isSaleCredit = response.data.isSaleCredit
                            root.defaultSetting.isPurchaseCredit = response.data.isPurchaseCredit
                            root.defaultSetting.isCustomeCredit = response.data.isCustomeCredit
                            root.defaultSetting.isCustomerPayCredit = response.data.isCustomerPayCredit
                            root.defaultSetting.isSupplierPayCredit = response.data.isSupplierPayCredit
                            root.defaultSetting.isSupplierCredit = response.data.isSupplierCredit
                            root.defaultSetting.isCashCustomer = response.data.isCashCustomer
                        }
                    }).catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
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
            this.cashCustomer = localStorage.getItem('CashCustomer');
        }
    })
</script>