<template>
    <div class="row" v-if="isValid('CanViewSupplier')">


        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Supplier.ListOfSupplier') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Supplier.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Supplier.ListOfSupplier') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-on:click="ImportDataFromXlsx" href="javascript:void(0);"
                                    v-if="isValid('CanImportSupplier') && ((rowCount < limitedSupplier) || (limitedSupplier == 0))"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Supplier.ImportSupplier') }}
                                </a>
                                <a v-if="isValid('CanAddSupplier') && ((rowCount < limitedSupplier) || (limitedSupplier == 0))"
                                    v-on:click="AddSupplier" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Supplier.AddNew') }}
                                </a>
                                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="DowmloadCSV()">
                                    Export Supplier Excel 
                                    <i class="fas fa-file-pdf float-right"></i>
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Supplier.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-8" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                    :placeholder="$t('Supplier.Search')" aria-label="Example text with button addon"
                                    aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button" class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div v-if="isRaw == 'true'">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == false }"
                                    v-on:click="makeActive(false)" data-bs-toggle="tab" href="#link6" role="tab"
                                    aria-selected="true">{{ $t('Supplier.Supplier') }}</a></li>
                            <li class="nav-item"><a class="nav-link" v-bind:class="{ active: active == true }"
                                    v-on:click="makeActive(true)" data-bs-toggle="tab" href="#link5" role="tab"
                                    aria-selected="false">{{ $t('Supplier.RawSupplier') }}</a></li>
                        </ul>
                    </div>
                    <div class="tab-content">
                        <div v-if="active == false">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <template v-for="item in supplierListView" >
                                                <th :key="item.id" v-if="item.name == 'code'">{{ $t('Customer.Code') }} </th>
                                                <th :key="item.id" v-else-if="item.name == 'customerNameEn'">{{ $t('Supplier.SupplierName')}}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerNameAr'">{{ $t('Supplier.SupplierName')  }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'displayName'">{{ $t('AddCustomer.CustomerDisplayName') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'companyNameEn'">{{ $t('AddCustomer.CompanyName') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'companyNameAr'">{{ $t('AddCustomer.CompanyName') }} Arabic </th>
                                                <th :key="item.id" v-else-if="item.name == 'mobileNo'">{{ $t('Mobile No') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'whatsappNo'">{{ $t('WhatsApp No') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'registrationDate'">{{ $t('AddCustomer.RegistrationDate') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'date'">{{ $t('Date') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'email'">{{ $t('AddCustomer.Email') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'crn'">{{ $t('CRN') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'taxNo'">{{ $t('AddCustomer.VAT/NTN/Tax No') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'paymentTerms'">{{ $t('AddCustomer.PaymentTerms') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'address'">{{ $t('AddCustomer.Address') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerCode'">{{ $t('AddCustomer.CustomerCode') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerCategory'">{{ $t('Customer Category') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'runningBalance'">{{ $t('Supplier.RunningBalance') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerType'">{{ $t('Supplier.SupplierType') }}</th>
                                            </template>
                                            <th>{{ $t('Customer.Status') }}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(supplier) in supplierlist" v-bind:key="supplier.id">

                                            <template v-for="item in supplierListView" >
                                                <td :key="item.id" v-if="item.name == 'code'">
                                                    <strong v-if="isValid('CanEditSupplier')">
                                                        <a href="javascript:void(0)" v-on:click="EditSupplier(supplier.id)">
                                                            {{ supplier.code}}
                                                        </a>
                                                    </strong>
                                                    <span v-else>
                                                        {{ supplier.code }}
                                                    </span>
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerNameEn'">
                                                    {{  (supplier.englishName != null ) ? supplier.englishName : "---"}}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerNameAr'">
                                                    {{ supplier.arabicName }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'displayName'">
                                                    {{ supplier.customerDisplayName }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'companyNameEn'">
                                                    {{ supplier.companyNameEnglish }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'companyNameAr'">
                                                    {{ supplier.companyNameArabic }} 
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'mobileNo'">
                                                    {{ supplier.contactNo1 }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'whatsappNo'">
                                                    {{ supplier.whatsAppNumber }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'registrationDate'">
                                                    {{ supplier.registrationDate }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'date'">
                                                    {{ getDate(date) }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'email'">
                                                    {{ supplier.email }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'crn'">
                                                    {{ supplier.commercialRegistrationNo }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'taxNo'">
                                                    {{ supplier.vatNo }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'paymentTerms'">
                                                    <span v-if="supplier.paymentTerms == 'Cash'"> {{ supplier.paymentTerms }}</span>
                                                    <span v-else-if="supplier.paymentTerms == 'Credit' || supplier.paymentTerms == 'آجل'">
                                                        {{ supplier.paymentTerms }} <div>Limit({{ supplier.creditLimit }}),
                                                            Periods({{ supplier.creditPeriod }})</div>
                                                    </span>
                                                    <span v-else> {{ supplier.paymentTerms }}</span>
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'address'">
                                                    {{ supplier.address }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerCode'">
                                                    {{ supplier.customerCode }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerCategory'">
                                                    {{ supplier.category }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'runningBalance'">
                                                    {{ supplier.openingBalance >= 0 ? 'Dr ' +
                                                    supplier.openingBalance * +1 : 'Cr ' + supplier.openingBalance * (-1) }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerType'">
                                                    {{  getType(supplier.supplierType) }}
                                                </td>
                                            </template>

                                            
                                            <td>
                                                <span v-if="supplier.isActive"
                                                    class="badge badge-boxed  badge-outline-success">{{ $t('Customer.Active') }}</span>
                                                <span v-else
                                                    class="badge badge-boxed  badge-outline-danger">{{ $t('Customer.De-Active') }}</span>
                                            </td>
                                            <!--<td><a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="RemoveSupplier(supplier.id)"><i class=" fa fa-trash"></i></a></td>-->
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                        <div v-if="active == true">
                            <div class="table-responsive">
                                <table class="table mb-0">
                                    <thead class="thead-light table-hover">
                                        <tr>
                                            <template v-for="item in supplierListView" >
                                                <th :key="item.id" v-if="item.name == 'code'">{{ $t('Customer.Code') }} </th>
                                                <th :key="item.id" v-else-if="item.name == 'customerNameEn'">{{ $t('Supplier.SupplierName')}}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerNameAr'">{{ $t('Supplier.SupplierName')  }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'displayName'">{{ $t('AddCustomer.CustomerDisplayName') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'companyNameEn'">{{ $t('AddCustomer.CompanyName') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'companyNameAr'">{{ $t('AddCustomer.CompanyName') }} Arabic </th>
                                                <th :key="item.id" v-else-if="item.name == 'mobileNo'">{{ $t('Mobile No') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'whatsappNo'">{{ $t('WhatsApp No') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'registrationDate'">{{ $t('AddCustomer.RegistrationDate') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'date'">{{ $t('Date') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'email'">{{ $t('AddCustomer.Email') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'crn'">{{ $t('CRN') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'taxNo'">{{ $t('AddCustomer.VAT/NTN/Tax No') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'paymentTerms'">{{ $t('AddCustomer.PaymentTerms') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'address'">{{ $t('AddCustomer.Address') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerCode'">{{ $t('AddCustomer.CustomerCode') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerCategory'">{{ $t('Customer Category') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'runningBalance'">{{ $t('Supplier.RunningBalance') }}</th>
                                                <th :key="item.id" v-else-if="item.name == 'customerType'">{{ $t('Supplier.SupplierType') }}</th>
                                            </template>
                                            <th>{{ $t('Customer.Status') }}</th>

                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(supplier) in supplierlist" v-bind:key="supplier.id">
                                            <template v-for="item in supplierListView" >
                                                <td :key="item.id" v-if="item.name == 'code'">
                                                    <strong v-if="isValid('CanEditSupplier')">
                                                        <a href="javascript:void(0)" v-on:click="EditSupplier(supplier.id)">
                                                            {{ supplier.code}}
                                                        </a>
                                                    </strong>
                                                    <span v-else>
                                                        {{ supplier.code }}
                                                    </span>
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerNameEn'">
                                                    {{  (supplier.englishName != null ) ? supplier.englishName : "---"}}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerNameAr'">
                                                    {{ supplier.arabicName }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'displayName'">
                                                    {{ supplier.customerDisplayName }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'companyNameEn'">
                                                    {{ supplier.companyNameEnglish }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'companyNameAr'">
                                                    {{ supplier.companyNameArabic }} 
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'mobileNo'">
                                                    {{ supplier.contactNo1 }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'whatsappNo'">
                                                    {{ supplier.whatsAppNumber }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'registrationDate'">
                                                    {{ supplier.registrationDate }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'date'">
                                                    {{ getDate(date) }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'email'">
                                                    {{ supplier.email }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'crn'">
                                                    {{ supplier.commercialRegistrationNo }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'taxNo'">
                                                    {{ supplier.vatNo }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'paymentTerms'">
                                                    <span v-if="supplier.paymentTerms == 'Cash'"> {{ supplier.paymentTerms }}</span>
                                                    <span v-else-if="supplier.paymentTerms == 'Credit' || supplier.paymentTerms == 'آجل'">
                                                        {{ supplier.paymentTerms }} <div>Limit({{ supplier.creditLimit }}),
                                                            Periods({{ supplier.creditPeriod }})</div>
                                                    </span>
                                                    <span v-else> {{ supplier.paymentTerms }}</span>
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'address'">
                                                    {{ supplier.address }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerCode'">
                                                    {{ supplier.customerCode }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerCategory'">
                                                    {{ supplier.category }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'runningBalance'">
                                                    {{ supplier.openingBalance >= 0 ? 'Dr ' +
                                                    supplier.openingBalance * +1 : 'Cr ' + supplier.openingBalance * (-1) }}
                                                </td>
                                                <td :key="item.id" v-else-if="item.name == 'customerType'">
                                                    {{  getType(supplier.supplierType) }}
                                                </td>
                                            </template>
                                            <td>
                                                <span v-if="supplier.isActive"
                                                    class="badge badge-boxed  badge-outline-success">{{ $t('Customer.Active') }}</span>
                                                <span v-else
                                                    class="badge badge-boxed  badge-outline-danger">{{ $t('Customer.De-Active') }}</span>
                                            </td>
                                            <!--<td><a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="RemoveSupplier(supplier.id)"><i class=" fa fa-trash"></i></a></td>-->
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }} {{ currentPage }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to') }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{ (currentPage * 10) - 9 }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="getPage()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                :next-text="$t('Table.Next')" :last-text="$t('Table.Last')">
                            </b-pagination>
                        </div>
                    </div>

                </div>
            </div>
            <loading v-model:active="loading"  :can-cancel="true" :is-full-page="true"></loading>

        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
import moment from "moment";
export default {
    name: 'supplier',
    mixins: [clickMixin],
    components: {
        Loading
    },
    data: function () {
        return {
            isRaw: '',
            supplierlist: [],
            supplier: [],
            loading: false,
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            search: '',
            arabic: '',
            english: '',
            active: false,
            language: '',
            limitedSupplier: 0,

            selected: [],
            selectAll: false,
            customerListView : [],
            supplierListView : [],
        }
    },
    watch: {
        // search: function (val) {
        //     this.GetSupplierData(val, 1, this.active);
        // }
    },
    methods: {
        getDate: function (date) {
            if (date == null || date == undefined) {
                return "";
            }
            else {
                return moment(date).format('LLL');
            }
        },
        search22: function () {
            this.GetSupplierData(this.search, this.currentPage, this.active);
        },

        clearData: function () {
            this.search = "";
            this.GetSupplierData(this.search, this.currentPage, this.active);

        },

        DowmloadCSV: function () {
                var root = this;
                var token = '';
                root.loading = true;

                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

                var paymentTerms = this.paymentTerm == 'Credit' || this.paymentTerm == 'آجـل' ? 'Credit' : '';
                
                root.$https.get('/Contact/GetContactListExcel?IsDropDown=' + true + '&isCustomer=' + false + '&isActive=' + true + '&paymentTerms=' + paymentTerms + '&multipleAddress=' + multipleAddress, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'Supplier Report.xlsx');
                        document.body.appendChild(link);
                        link.click();
                        root.loading = false;

                    }).catch(error => {
                        console.log(error);
                        root.loading = false;
                        root.$swal.fire({
                            icon: 'error',
                            type: 'error',
                            title: root.$t('Error'),
                            text: root.$t('Something went Wrong'),
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });
                    });
            },


        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        select: function () {
            this.selected = [];
            if (!this.selectAll) {
                for (let i in this.supplierlist) {
                    this.selected.push(this.supplierlist[i].id);
                }
            }
        },

        ImportDataFromXlsx: function () {
            var root = this;
            root.$router.push({
                path: '/ImportExportRecords',
                query: { data: 'Supplier' }
            })
        },
        getType: function (x) {
            this.language = this.$i18n.locale;

            if (this.language == 'en') {
                if (x == 1) {
                    return 'Wholesaler';
                }
                else if (x == 2) {
                    return 'Retailer';
                }
                else if (x == 5) {
                    return 'Wholesaler & Retailer';
                }
                else if (x == 3) {
                    return 'Dealer';
                }
                else if (x == 4) {
                    return 'Distributor';
                }
                else {
                    return '';
                }
            }
            else {
                if (x == 1) {
                    return 'جمله';
                }
                else if (x == 2) {
                    return 'قطاعي';
                }
                else if (x == 5) {
                    return 'بائع بالجملة';
                }
                else if (x == 3) {
                    return 'وكيل';
                }
                else if (x == 4) {
                    return 'موزع';
                }
                else {
                    return '';
                }
            }

        },
        AddSupplier: function () {


            this.$router.push({
                    path: '/addCustomer',
                    query: {
                        formName: 'Supplier',

                    }
                })


        },
        makeActive: function (item) {

            this.active = item;
            this.GetSupplierData(this.search, 1, item);
        },
        getPage: function () {
            this.GetSupplierData(this.search, this.currentPage, this.active);
        },
        EditSupplier: function (Id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

            this.$https.get('/Contact/ContactDetail?Id=' + Id+ '&multipleAddress=' + multipleAddress, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.supplier = response.data
                    root.$store.GetEditObj(root.supplier);
                }
                root.$router.push({
                    path: '/AddCustomer',
                    query: {
                        formName: 'Supplier',
                        data: '',
                        Add: false
                    }
                })
            });
        },

        GetSupplierData: function (search, currentPage, status) {

            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Contact/ContactList?isCustomer=false' + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&status=' + status, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {



                    root.supplierlist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                }
            });
        },
        RemoveSupplier: function (id) {
            var root = this;
            // working with IE and Chrome both

            this.$swal({
                icon: 'error',
                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!',
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                closeOnConfirm: false,
                closeOnCancel: true
            }).then(function (result) {
                if (result) {

                    var token = '';
                    if (root.$session.exists()) {
                        token = localStorage.getItem('token');
                    }
                    root.$https
                        .get('/Contact/ContactDelete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {

                            if (response.data.id != '00000000-0000-0000-0000-000000000000') {
                                root.$store.state.supplierlist.splice(root.$store.state.supplierlist.findIndex(function (i) {
                                    return i.id === response.data;
                                }), 1);
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                    text: response.data.isAddUpdate,
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonsStyling: false
                                });
                            } else {
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: response.data.isAddUpdate,
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false
                                });
                            }
                        },
                            function () {
                                root.loading = false;
                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false
                                });
                            });
                }
                else {
                    this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                }
            });
        },
        GetItemViewSetup: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/GetItemOrderList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.customerListView = [];
                    root.supplierListView = [];
                    root.customerListView = response.data.customerListView;
                    root.supplierListView = response.data.supplierListView;
                }
            });
        },
    },
    created: function () {

        this.$emit('input', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.isRaw = localStorage.getItem('IsProduction');
        this.limitedSupplier = localStorage.getItem('LimitedSupplier');
        if (isNaN(this.limitedSupplier)) {
            this.limitedSupplier = 0
        }
        this.makeActive(false);
        this.GetItemViewSetup();
    },
    updated: function () {
        if (this.selected.length < this.supplierlist.length) {
            this.selectAll = false;
        }
        else if (this.selected.length == this.supplierlist.length) {
            if (this.selected.length == 0) {
                this.selectAll = false;
            }
            else {
                this.selectAll = true
            }
        }
    }
}
</script>