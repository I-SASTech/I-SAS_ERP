<template>
    <!--Page-Title-->
    <div class="container-fluid">
        <div class="row">
            <div class="col-12">
                <div class="text-center" style="margin-top: 150px;">
                    <i class="la la-sun-o text-primary la-spin progress-icon-spin" style="font-size: 80px;"></i>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment';
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        data: function () {
            return {
                kg: '',
                role: '',
                dashboard: [],
                isProceed: false,
                isAccount: false,
                terms: false,
                isEmployee: true,
            }
        },
        methods: {
            GetInvoiceDefault: function () {
                this.loading = true;
                var root = this;
                var token = '';
               /* if (token == '') {*/
                    token = localStorage.getItem('token');
          /*      }*/
                this.$https.get('/Company/InvoiceDefaultDetails', { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {


                        if (response.data != null) {

                            localStorage.setItem('IsSalePrice', response.data.isSalePrice);
                            localStorage.setItem('IsSalePriceLabel', response.data.isSalePriceLabel);
                            localStorage.setItem('IsCustomerPrice', response.data.isCustomerPrice);
                            localStorage.setItem('IsCustomerPriceLabel', response.data.isCustomerPriceLabel);
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
            getDate: function (date) {
                return moment(date).format('LL');
            },

            GoTo: function (link, token) {
                this.$router.push({
                    path: link,
                    query: {
                        token_name: token,
                        fromDashboard: 'true'
                    }
                });
            },

            handleWeekendsToggle() {
                this.calendarOptions.weekends = !this.calendarOptions.weekends // update a property
            },

            GetModuleWiseClaims: function (user) {
                var root = this;
                var token = '';
               /* if (token == '') {*/
                    token = localStorage.getItem('token');
             /*   }*/

                this.$https.get('/Company/GetModuleWiseClaims?branchId=' +localStorage.getItem('BranchId'),  { headers: { "Authorization": `Bearer ${token}`} }).then(function (response) {

                    if (response.data != null) {

                        localStorage.setItem('SimpleToken', token)
                        response.data.forEach(function (x) {
                            localStorage.setItem(x.tokenName + '_token', x.token)
                        });
                        root.GetCompanyPermission(response.data.userRoleName, response.data.touchScreen, response.data.isDayStart);
                        root.GetCurrency();
                        root.GetUserPermission(user);
                    }
                });
            },

            GetCurrency: function () {
                var token = '';
            /*    if (token == '') {*/
                    token = localStorage.getItem('token');
     /*           }*/
                this.$https.get('/Account/GetCompanySetupAccount', { headers: {"Authorization": `Bearer ${token}`} }).then(function (response) {
                    if (response.data != null) {
                        localStorage.setItem('currency', response.data.currency);
                        localStorage.setItem('taxMethod', response.data.taxMethod);
                        localStorage.setItem('TaxRateId', response.data.taxRateId);
                        localStorage.setItem('DiscountTypeOption', response.data.discountTypeOption);
                    }
                });
            },

            GetUserPermission: function (x) {
                var token = '';
               /* if (token == '') {*/
                    token = localStorage.getItem('token');
               /* }*/
                this.$https.get('/Account/GetUserPermission?id=' + x, { headers: { "Authorization": `Bearer ${token}` }}).then(function (response) {

                    if (response.data != null) {
                        localStorage.setItem('changePriceDuringSale', response.data.changePriceDuringSale);
                        localStorage.setItem('giveDicountDuringSale', response.data.giveDicountDuringSale);
                        //    localStorage.setItem('InvoiceWoInventoryUser', response.data.invoiceWoInventory);
                    }
                });
            },

            GetCompanyPermission: function (name, isTouch, dayStart) {
                var root = this;
                var token = '';
               /* if (token == '') {*/
                    token = localStorage.getItem('token');
            /*    }*/
                this.$https.get('/Company/GetCompanyPermission?branchId=' +localStorage.getItem('BranchId'), { headers: {"Authorization": `Bearer ${token}` }}).then(function (response) {
                    if (response.data != null) {
                        if (response.data.length == 0) {
                            root.$router.push({
                                path: '/NotPermission',
                                query: {
                                    data: 'You do not have any permission. Please contact to support.'
                                }
                            });
                        }
                        else {
                            localStorage.setItem("permission", JSON.stringify(response.data));
                            localStorage.setItem('LimitedCustomer', 0);
                            localStorage.setItem('LimitedSupplier', 0);
                            localStorage.setItem('CashVoucher', false);
                            localStorage.setItem('isForMedical', false);
                            localStorage.setItem('InvoiceWoInventory', false);
                            localStorage.setItem('SoInventoryReserve', false);
                            localStorage.setItem('b2c', false);
                            localStorage.setItem('b2b', false);
                            localStorage.setItem('ExpenseToGst', false);
                            localStorage.setItem('AutoPurchaseVoucher', false);
                            localStorage.setItem('PartiallyPurchase', false);
                            localStorage.setItem('InternationalPurchase', false);
                            localStorage.setItem('coaCode', false);
                            localStorage.setItem('IsExpenseAccount', false);
                            localStorage.setItem('DayStart', false);
                            localStorage.setItem('IsOpenDay', false);
                            localStorage.setItem('IsTransferAllow', false);
                            localStorage.setItem('IsMultiUnit', false);
                            localStorage.setItem('CashCustomer', false);
                            localStorage.setItem('CanSelectWarehouse', false);
                            localStorage.setItem('IsArea', false);
                            localStorage.setItem('English', false);
                            localStorage.setItem('Arabic', false);
                            localStorage.setItem('Portugues', false);
                            localStorage.setItem('LeftToRight', false);
                            localStorage.setItem('SuperAdminDayStart', false);
                            localStorage.setItem('BankDetail', false);
                            localStorage.setItem('decimalQuantity', false);
                            localStorage.setItem('expenseAmount', false);
                            localStorage.setItem('IsProduction', false);
                            localStorage.setItem('IsSerial', false);
                            localStorage.setItem('fIFO', false);
                            localStorage.setItem('ChequeDateRequired', false);
                            localStorage.setItem('ValidityDateAuto', false);
                            localStorage.setItem('CanViewUnitPerPack', false);
                            localStorage.setItem('ServicePurchase', false);
                            localStorage.setItem('ColorVariants', false);
                            localStorage.setItem('IsPaksitanClient', false);
                            localStorage.setItem('purchaseNumber', '');
                            //localStorage.setItem('DefaultVat', 'DefaultVat');
                            //localStorage.setItem('DefaultVat', 'DefaultVatHead');
                            //localStorage.setItem('DefaultVat', 'DefaultVatItem');
                            /*localStorage.setItem('DefaultVat', 'DefaultVatHeadItem');*/
                            //localStorage.setItem('SaleDefaultVat', 'DefaultVat');
                            //localStorage.setItem('SaleDefaultVat', 'DefaultVatHead');
                            /*localStorage.setItem('SaleDefaultVat', 'DefaultVatItem');*/
                            //localStorage.setItem('SaleDefaultVat', 'DefaultVatHeadItem');

                            localStorage.setItem('VatSelectOnSale', false);
                            localStorage.setItem('ImportExportSale', false);
                            localStorage.setItem('WholeSalePriceActivation', false);

                            localStorage.setItem('PosWithTransactionlevel', false);
                            localStorage.setItem('CreditCustomerLedger', false);
                            localStorage.setItem('CreditSupplierLedger', false);
                            localStorage.setItem('ExpenseLedger', false);
                            localStorage.setItem('InvoiceAndCostLedger', false);
                            localStorage.setItem('ClearingAgentLedger', false);
                            localStorage.setItem('AllowPreviousFinancialPeriod', false);
                            localStorage.setItem('CreateDocument', false);
                            localStorage.setItem('MultipleAddress', false);
                            localStorage.setItem('PaymentOptions', false);
                            localStorage.setItem('AllowBranches', false);
                            localStorage.setItem('AdvancePayment', false);
                            localStorage.setItem('BranchType', '');
                            
                            response.data.forEach(function (x) {

                                if (x.moduleDescription === '3a0a2ddf-0773-4746-913d-0ef1397cc14f') {
                                    switch (x.permissionCategory) {
                                        case 'AllowBranches':
                                            localStorage.setItem('AllowBranches', true);
                                            break;
                                        case 'AdvancePayment':
                                            localStorage.setItem('AdvancePayment', true);
                                            break;
                                        case 'SimpleBranches':
                                            localStorage.setItem('BranchType', 'Simple Branches');
                                            break;
                                        case 'BranchesWithAccount':
                                            localStorage.setItem('BranchType', 'Branches With Account');
                                            break;
                                        case 'BranchAsFranchise':
                                            localStorage.setItem('BranchType', 'Branch As Franchise');
                                            break;
                                        case 'CashVoucher':
                                            localStorage.setItem('CashVoucher', true);
                                            break;
                                        case 'Medical':
                                            localStorage.setItem('isForMedical', true);
                                            break;
                                        case 'InvoiceWoInventory':
                                            localStorage.setItem('InvoiceWoInventory', true);
                                            break;
                                        case 'SaleOrderInventoryReserve':
                                            localStorage.setItem('SoInventoryReserve', true);
                                            break;
                                        case 'B2B':
                                            localStorage.setItem('b2b', true);
                                            break;
                                        case 'B2C':
                                            localStorage.setItem('b2c', true);
                                            break;
                                        case 'ExpenseToGST':
                                            localStorage.setItem('ExpenseToGst', true);
                                            break;
                                        case 'AutoPurchaseVoucher':
                                            localStorage.setItem('AutoPurchaseVoucher', true);
                                            break;
                                        case 'PartiallyPurchase':
                                            localStorage.setItem('PartiallyPurchase', true);
                                            break;
                                        case 'InternationalPurchase':
                                            localStorage.setItem('InternationalPurchase', true);
                                            break;
                                        case 'CoaAutoCode':
                                            localStorage.setItem('coaCode', true);
                                            break;
                                        case 'ExpenseWithAccount':
                                            localStorage.setItem('IsExpenseAccount', true);
                                            break;
                                        case 'CanStartDay':
                                            localStorage.setItem('DayStart', true);
                                            break;
                                        case 'IsOpenDay':
                                            localStorage.setItem('IsOpenDay', true);
                                            break;
                                        case 'CanTransferCounter':
                                            localStorage.setItem('IsTransferAllow', true);
                                            break;
                                        case 'MultiUnit':
                                            localStorage.setItem('IsMultiUnit', true);
                                            break;
                                        case 'CanSelectWarehouse':
                                            localStorage.setItem('CanSelectWarehouse', true);
                                            break;
                                        case 'CashCustomer':
                                            localStorage.setItem('CashCustomer', true);
                                            break;
                                        case 'IsArea':
                                            localStorage.setItem('IsArea', true);
                                            break;
                                        case 'English':
                                            localStorage.setItem('English', true);
                                            //localStorage.setItem('locales', 'en');
                                            break;
                                        case 'Arabic':
                                            localStorage.setItem('Arabic', true);
                                            //localStorage.setItem('locales', 'ar');
                                            break;
                                        case 'Portugues':
                                            localStorage.setItem('Portugues', true);
                                            break;
                                        case 'LeftToRight':
                                            localStorage.setItem('LeftToRight', true);
                                            break;
                                        case 'Terminal':
                                            //localStorage.setItem('BankDetail', true);
                                            break;
                                        case 'AdminDayStart':
                                            localStorage.setItem('SuperAdminDayStart', true);
                                            break;
                                        case 'BankDetail':
                                            localStorage.setItem('BankDetail', true);
                                            break;
                                        case 'DecimalQuantity':
                                            localStorage.setItem('decimalQuantity', true);
                                            break;
                                        case 'ExpenseAmountExceed':
                                            localStorage.setItem('expenseAmount', true);
                                            break;
                                        case 'Production':
                                            localStorage.setItem('IsProduction', true);
                                            break;
                                        case 'فاتورة ضريبية':
                                            localStorage.setItem('taxInvoiceLabelAr', 'فاتورة ضريبية');
                                            break;
                                        case 'Tax Invoice':
                                            localStorage.setItem('taxInvoiceLabel', 'Tax Invoice');
                                            break;
                                        case 'فاتورة ضريبية مبسطة':
                                            localStorage.setItem('simplifyTaxInvoiceLabelAr', 'فاتورة ضريبية مبسطة');
                                            break;
                                        case 'Simplified Tax Invoice':
                                            localStorage.setItem('simplifyTaxInvoiceLabel', 'Simplified Tax Invoice');
                                            break;
                                        case 'IsSerial':
                                            localStorage.setItem('IsSerial', true);
                                            break;
                                        case 'IsFifo':
                                            localStorage.setItem('fIFO', true);
                                            break;
                                        case 'ChequeDateRequired':
                                            localStorage.setItem('ChequeDateRequired', true);
                                            break;
                                        case 'ValidityDateAuto':
                                            localStorage.setItem('ValidityDateAuto', true);
                                            break;
                                        case 'CanViewUnitPerPack':
                                            localStorage.setItem('CanViewUnitPerPack', true);
                                            break;
                                        case 'ImportExportSale':
                                            localStorage.setItem('ImportExportSale', true);
                                            break;
                                        case 'ServicePurchase':
                                            localStorage.setItem('ServicePurchase', true);
                                            break;

                                        case 'VatSelectOnSale':
                                            localStorage.setItem('VatSelectOnSale', true);
                                            break;
                                        case 'WholeSalePriceActivation':
                                            localStorage.setItem('WholeSalePriceActivation', true);
                                            break;
                                        case 'DefaultSaleVat':
                                            localStorage.setItem('SaleDefaultVat', 'DefaultVat');
                                            break;
                                        case 'DefaultSaleVatHead':
                                            localStorage.setItem('SaleDefaultVat', 'DefaultVatHead');
                                            break;
                                        case 'DefaultSaleVatItem':
                                            localStorage.setItem('SaleDefaultVat', 'DefaultVatItem');
                                            break;
                                        case 'DefaultSaleVatHeadItem':
                                            localStorage.setItem('SaleDefaultVat', 'DefaultVatHeadItem');
                                            break;
                                        case 'DefaultPurchaseVat':
                                            localStorage.setItem('DefaultVat', 'DefaultVat');
                                            break;
                                        case 'DefaultPurchaseVatHead':
                                            localStorage.setItem('DefaultVat', 'DefaultVatHead');
                                            break;
                                        case 'DefaultPurchaseVatItem':
                                            localStorage.setItem('DefaultVat', 'DefaultVatItem');
                                            break;
                                        case 'DefaultPurchaseVatHeadItem':
                                            localStorage.setItem('DefaultVat', 'DefaultVatHeadItem');
                                            break;
                                        case 'IsPaksitanClient':
                                            localStorage.setItem('IsPaksitanClient', true);
                                            break;
                                        case 'OnlineUser':
                                            localStorage.setItem('TerminalUserType', 'Online');
                                            break;
                                        case 'OfflineUser':
                                            localStorage.setItem('TerminalUserType', 'Offline');
                                            break;
                                        case 'BothUser':
                                            localStorage.setItem('TerminalUserType', 'Both');
                                            break;
                                        case 'MachineWisePrefix':
                                            localStorage.setItem('InvoicePrefix', 'MachineWisePrefix');
                                            break;
                                        case 'EmployeeWisePrefix':
                                            localStorage.setItem('InvoicePrefix', 'EmployeeWisePrefix');
                                            break;
                                        case 'NormalPrefix':
                                            localStorage.setItem('InvoicePrefix', 'NormalPrefix');
                                            break;
                                        case 'SimpleSaleInvoice':
                                            localStorage.setItem('IsSimpleInvoice', true);
                                            break;
                                        case 'SaleServiceInvoice':
                                            localStorage.setItem('IsSimpleInvoice', false);
                                            break;

                                        case 'PosWithTransactionlevel':
                                            localStorage.setItem('PosWithTransactionlevel', true);
                                            break;
                                        case 'CreditCustomerLedger':
                                            localStorage.setItem('CreditCustomerLedger', true);
                                            break;
                                        case 'CreditSupplierLedger':
                                            localStorage.setItem('CreditSupplierLedger', true);
                                            break;
                                        case 'ClearingAgentLedger':
                                            localStorage.setItem('ClearingAgentLedger', true);
                                            break;
                                        case 'ExpenseLedger':
                                            localStorage.setItem('ExpenseLedger', true);
                                            break;
                                        case 'InvoiceAndCostLedger':
                                            localStorage.setItem('InvoiceAndCostLedger', true);
                                            break;
                                        case 'AllowPreviousFinancialPeriod':
                                            localStorage.setItem('AllowPreviousFinancialPeriod', true);
                                            break;
                                        case 'CreateDocument':
                                            localStorage.setItem('CreateDocument', true);
                                            break;
                                        case 'MultipleAddress':
                                            localStorage.setItem('MultipleAddress', true);
                                            break;
                                        case 'PaymentOptions':
                                            localStorage.setItem('PaymentOptions', true);
                                            break;
                                    }
                                    if (x.key === '7a6930e8-876c-4344-b14e-80b961c14f96') {
                                        localStorage.setItem('attachmentLimit', x.permissionCategory);
                                    }

                                    if (x.key === '8cb9768b-f76c-4614-a8a8-c22c7f1a0c81') {
                                        localStorage.setItem('openBatch', x.permissionCategory);
                                    }

                                    if (x.key === '671b5fb5-94d5-4edd-89fa-58cb08da4783') {
                                        localStorage.setItem('attachmentSize', x.permissionCategory);
                                    }
                                } else if (x.key === '7dc50e60-d5a2-419a-b12a-200ac71d7cb6') {
                                    localStorage.setItem('LimitedCustomer', x.permissionCategory);
                                } else if (x.key === '3d1f65f1-3f72-4898-a175-1b6ab42b2b9d') {
                                    localStorage.setItem('LimitedSupplier', x.permissionCategory);
                                }
                            })

                            var isEnglish = response.data.findIndex(x => x.moduleDescription === '3a0a2ddf-0773-4746-913d-0ef1397cc14f' && x.permissionCategory === 'English')
                            var isArabic = response.data.findIndex(x => x.moduleDescription === '3a0a2ddf-0773-4746-913d-0ef1397cc14f' && x.permissionCategory === 'Arabic')

                            var getLocale = localStorage.getItem('locales');

                            if (isEnglish >= 0 && isArabic < 0) {
                                localStorage.setItem('locales', 'en');
                            } else if (isEnglish < 0 && isArabic >= 0) {
                                localStorage.setItem('locales', 'ar');
                            } else {
                                if (getLocale == 'ar') {
                                    localStorage.setItem('locales', 'ar');
                                } else {
                                    localStorage.setItem('locales', 'en');
                                }
                            }
                            //if (root.isPayment) {
                            //    root.$swal.fire(
                            //        {
                            //            toast: true,
                            //            position: 'bottom-end',
                            //            showConfirmButton: false,
                            //            timer: 10000,
                            //            timerProgressBar: true,
                            //            icon: 'error',
                            //            title: root.expirationMsg,
                            //            didOpen: (toast) => {
                            //                toast.addEventListener('mouseenter', root.$swal.stopTimer)
                            //                toast.addEventListener('mouseleave', root.$swal.resumeTimer)
                            //            }
                            //        });
                            //}
                            //else {
                            //    root.$swal.fire(
                            //        {
                            //            toast: true,
                            //            position: 'top-end',
                            //            showConfirmButton: false,
                            //            timer: 1500,
                            //            timerProgressBar: true,
                            //            icon: 'success',
                            //            title: 'Logged in Successfully',
                            //            didOpen: (toast) => {
                            //                toast.addEventListener('mouseenter', root.$swal.stopTimer)
                            //                toast.addEventListener('mouseleave', root.$swal.resumeTimer)
                            //            }
                            //        });
                            //}

                            if (!root.isProceed) {
                                root.$router.push('/setup');
                            }

                            else if (root.isEmployee == true) {
                                if (name == 'Sales Man') {
                                    if (dayStart == true) {
                                        if (isTouch == 'Touch Invoice') {
                                            root.$router.push('/TouchScreen');
                                        } else {
                                            root.$router.push('/TouchScreen');
                                        }
                                    }
                                } else {
                                    root.$router.push('/StartScreen');
                                }
                            }
                        }
                    }
                });
            },

            getWhiteLabeling: function () {
                //var root = this;
                this.$https.get('/account/GetWhiteLabeling').then(function (response) {
                    if (response.data != null) {
                        localStorage.setItem('CompanyName', response.data.companyName);
                        localStorage.setItem('ApplicationName', response.data.applicationName);
                        localStorage.setItem('favName', response.data.favName);
                        localStorage.setItem('email', response.data.email);
                    }
                });
            },
        },

        created: function () {

            this.isProceed = localStorage.getItem('isProceed') == 'true' ? true : false;
            this.role = localStorage.getItem('RoleName');
            var userId = localStorage.getItem('UserId');
            this.getWhiteLabeling();
            this.GetModuleWiseClaims(userId);

            if (this.isValid('IsPricing')) {
                this.GetInvoiceDefault();
            }
        },

    }
</script>
