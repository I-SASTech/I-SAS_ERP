<template>
    <div class="row" v-if="isValid('CanViewCustomer')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Customer.Customer') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item">
                                        <a href="javascript:void(0);">{{ $t('Customer.Home') }}</a>
                                    </li>
                                    <li class="breadcrumb-item active">{{ $t('Customer.Customer') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddCustomer') && ((rowCount < limitedCustomer) || (limitedCustomer == 0))"
                                   v-on:click="AddCustomer" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Customer.AddNew') }}
                                </a>
                                <a v-on:click="ImportDataFromXlsx" href="javascript:void(0);"
                                   v-if="isValid('CanImportCustomer') && ((rowCount < limitedCustomer) || (limitedCustomer == 0))"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Customer.ImportCustomer') }}
                                </a>
                                <a class="btn btn-sm btn-outline-primary mx-1" v-on:click="DowmloadCSV()">
                                    Export Cash/Credit Customer Excel
                                    <i class="fas fa-file-pdf float-right"></i>
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Customer.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="row">
                        <div class="col-lg-6" style="padding-top:20px">
                            <div class="input-group">
                                <button class="btn btn-secondary" type="button" id="button-addon1">
                                    <i class="fas fa-search"></i>
                                </button>
                                <input v-model="search" type="text" class="form-control"
                                       :placeholder="$t('Customer.SearchbyBrand')" aria-label="Example text with button addon"
                                       aria-describedby="button-addon1">
                                <a class="btn btn-soft-primary" v-on:click="AdvanceFilterFor" id="button-addon2"
                                   value="Advance Filter">
                                    <i class="fa fa-filter"></i>
                                </a>
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">
                            <button v-on:click="search22(true)" :disabled="!search" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled="!search" type="button"
                                    class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>
                        </div>
                    </div>
                    <div class="row " v-if="advanceFilters">
                        <div class="col-lg-4">
                            <label class="text  font-weight-bolder">{{ $t('Sale.Customer') }}</label>
                            <customerdropdown v-model="customerId" ref="CustomerDropdown" />
                        </div>
                        <div class="col-lg-4">
                            <label>{{ $t('AddCustomer.PaymentTerms') }}</label>
                            <div class="form-group">
                                <multiselect v-model="paymentTerms2" :preselect-first="true"
                                             v-if="($i18n.locale == 'en' || isLeftToRight())" :options="['Cash', 'Credit']"
                                             :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                                </multiselect>
                                <multiselect v-else v-model="paymentTerms2" :preselect-first="true" :options="['نقد', 'آجل']"
                                             :show-labels="false" v-bind:placeholder="$t('AddCustomer.SelectOption')">
                                </multiselect>
                            </div>
                        </div>
                        <div class=" col-lg-4">
                            <label>{{ $t('AddCustomer.CustomerCategory') }}: </label>

                            <multiselect v-if="($i18n.locale == 'en' || isLeftToRight())" v-model="customerCategory"
                                         :options="['Individual', 'Establishment', 'Company', 'Factory', 'Manufacturer', 'Government']"
                                         :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                            </multiselect>
                            <multiselect v-else v-model="customerCategory"
                                         :options="['فرد', 'مؤسسة', 'شركة', 'مصنع', 'الصانع', 'حكومة']" :show-labels="false"
                                         v-bind:placeholder="$t('AddCustomer.SelectOption')">
                            </multiselect>

                        </div>
                        <div class="col-lg-4">
                            <label>{{ $t('AddCustomer.CustomerGroup') }}</label>

                            <customergroupdropdown v-model="customerGroupId" :modelValue="customerGroupId"
                                                   ref="CustomerGroupDropdown" />

                        </div>

                        <div class="col-lg-4 mt-4">
                            <span id="ember694" class="tooltip-container text-dashed-underline ">
                                {{
                                $t('AddCustomer.CustomerType')
                                }} :
                            </span>

                            <div class="form-check form-check-inline">
                                <input v-model="category" name="contact-sub-type" id="a49946497" class=" form-check-input"
                                       type="radio" value="B2B – Business to Business">
                                <label class="form-check-label pl-0" for="a49946497">
                                    {{ $t('AddCustomer.Business') }}({{
                                    $t('AddCustomer.B2B')
                                    }})
                                </label>
                            </div>
                            <div class="form-check form-check-inline">
                                <input v-model="category" name="contact-sub-type" id="a9ff8eb35" class=" form-check-input"
                                       type="radio" value="B2C – Business to Client">
                                <label class="form-check-label pl-0" for="a9ff8eb35">
                                    {{
 $t('AddCustomer.Individual')
                                    }}({{ $t('AddCustomer.B2C') }})
                                </label>
                            </div>
                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3">

                            <button v-on:click="FilterRecord(true)" :disabled="!isFilterButtonDisabled" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="FilterRecord(false)" :disabled="!isFilterButtonDisabled" type="button"
                                    class="btn btn-outline-primary mx-2 mt-3">
                                {{ $t('Sale.ClearFilter') }}
                            </button>

                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div v-if="isCashCustomer">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link" v-bind:class="{ active: active == false }"
                                   v-on:click="makeActive(false)" data-bs-toggle="tab" href="#link6" role="tab"
                                   aria-selected="true">Cash/Credit</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" v-bind:class="{ active: active == true }"
                                   v-on:click="makeActive(true)" data-bs-toggle="tab" href="#link5" role="tab"
                                   aria-selected="false">Walk-In</a>
                            </li>
                        </ul>
                    </div>
                    <div class="table-responsive" v-if="active == false">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <template v-for="item in customerListView">
                                        <th :key="item.id" v-if="item.name == 'code'">{{ $t('Customer.Code') }} </th>
                                        <th :key="item.id" v-else-if="item.name == 'customerNameEn'">{{ $t('AddCustomer.CustomerName(English)')}}</th>
                                        <th :key="item.id" v-else-if="item.name == 'customerNameAr'">{{ $t('AddCustomer.CustomerName(Arabic)') }}</th>
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
                                        <th :key="item.id" v-else-if="item.name == 'runningBalance'">{{ $t('Running Balance') }}</th>
                                        <th :key="item.id" v-else-if="item.name == 'customerType'">{{ $t('AddCustomer.CustomerType') }}</th>
                                    </template>

                                    <th>{{ $t('Customer.Status') }}</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(cust, index) in customerlist" v-bind:key="cust.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <template v-for="item in customerListView">
                                        <td :key="item.id" v-if="item.name == 'code'">
                                            <strong v-if="isValid('CanEditCustomer')">
                                                <a href="javascript:void(0)" v-on:click="EditCustomer(cust.id)">
                                                    {{ cust.code}}
                                                </a>
                                            </strong>
                                            <span v-else>
                                                {{ cust.code }}
                                            </span>
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerNameEn'">
                                            {{  (cust.englishName != null ||cust.englishName != '' ) ? cust.englishName : "---"}}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerNameAr'">
                                            {{ cust.arabicName }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'displayName'">
                                            {{ cust.customerDisplayName }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'companyNameEn'">
                                            {{ cust.companyNameEnglish }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'companyNameAr'">
                                            {{ cust.companyNameArabic }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'mobileNo'">
                                            {{ cust.contactNo1 }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'whatsappNo'">
                                            {{ cust.whatsAppNumber }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'registrationDate'">
                                            {{ cust.registrationDate }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'date'">
                                            {{ getDate(date) }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'email'">
                                            {{ cust.email }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'crn'">
                                            {{ cust.commercialRegistrationNo }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'taxNo'">
                                            {{ cust.vatNo }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'paymentTerms'">
                                            {{ cust.paymentTerms }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'address'">
                                            {{ cust.address }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerCode'">
                                            {{ cust.customerCode }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerCategory'">
                                            {{ cust.category }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'runningBalance'">
                                            {{
 cust.openingBalance >= 0 ? 'Dr ' + cust.openingBalance * +1 :
                                            'Cr ' +
                                            cust.openingBalance * (-1)
                                            }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerType'">
                                            {{ cust.customerType }}
                                        </td>
                                    </template>

                                    <td>
                                        <span v-if="cust.isActive" class="badge badge-boxed  badge-outline-success">
                                            {{
                                            $t('Customer.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{
                                            $t('Customer.De-Active')
                                            }}
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="table-responsive" v-if="active == true">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <template v-for="item in customerListView">
                                        <th :key="item.id" v-if="item.name == 'code'">{{ $t('Customer.Code') }} </th>
                                        <th :key="item.id" v-else-if="item.name == 'customerNameEn'">{{ $t('AddCustomer.CustomerName(English)')}}</th>
                                        <th :key="item.id" v-else-if="item.name == 'customerNameAr'">{{ $t('AddCustomer.CustomerName(Arabic)') }}</th>
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
                                        <th :key="item.id" v-else-if="item.name == 'runningBalance'">{{ $t('Running Balance') }}</th>
                                        <th :key="item.id" v-else-if="item.name == 'customerType'">{{ $t('AddCustomer.CustomerType') }}</th>
                                    </template>
                                    <th>{{ $t('Customer.Status') }}</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(cust, index) in customerlist" v-bind:key="cust.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <template v-for="item in customerListView">
                                        <td :key="item.id" v-if="item.name == 'code'">
                                            <strong v-if="isValid('CanEditCustomer')">
                                                <a href="javascript:void(0)" v-on:click="EditCustomer(cust.id)">
                                                    {{ cust.code}}
                                                </a>
                                            </strong>
                                            <span v-else>
                                                {{ cust.code }}
                                            </span>
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerNameEn'">
                                            {{  (cust.englishName != null ) ? cust.englishName : "---"}}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerNameAr'">
                                            {{ cust.arabicName }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'displayName'">
                                            {{ cust.customerDisplayName }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'companyNameEn'">
                                            {{ cust.companyNameEnglish }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'companyNameAr'">
                                            {{ cust.companyNameArabic }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'mobileNo'">
                                            {{ cust.contactNo1 }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'whatsappNo'">
                                            {{ cust.whatsAppNumber }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'registrationDate'">
                                            {{ cust.registrationDate }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'date'">
                                            {{ getDate(date) }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'email'">
                                            {{ cust.email }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'crn'">
                                            {{ cust.commercialRegistrationNo }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'taxNo'">
                                            {{ cust.vatNo }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'paymentTerms'">
                                            {{ cust.paymentTerms }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'address'">
                                            {{ cust.address }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerCode'">
                                            {{ cust.customerCode }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerCategory'">
                                            {{ cust.category }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'runningBalance'">
                                            {{
 cust.openingBalance >= 0 ? 'Dr ' + cust.openingBalance * +1 :
                                            'Cr ' +
                                            cust.openingBalance * (-1)
                                            }}
                                        </td>
                                        <td :key="item.id" v-else-if="item.name == 'customerType'">
                                            {{ cust.customerType }}
                                        </td>
                                    </template>
                                    <td>
                                        <span v-if="cust.isActive" class="badge badge-boxed  badge-outline-success">
                                            {{
                                            $t('Customer.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{
                                            $t('Customer.De-Active')
                                            }}
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="float-start">
                        <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage === 1 && rowCount < 10">
                            {{ $t('Pagination.Showing') }} {{
 currentPage
                            }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries')
                            }}
                        </span>
                        <span v-else-if="currentPage === 1 && rowCount >= 11">
                            {{ $t('Pagination.Showing') }}
                            {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === 1">
                            {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                            $t('Pagination.to')
                            }} {{ currentPage * 10 }} of {{ rowCount }} {{
 $t('Pagination.entries')
                            }}
                        </span>
                        <span v-else-if="currentPage !== 1 && currentPage !== pageCount">
                            {{ $t('Pagination.Showing') }}
                            {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                $t('Pagination.of')
                            }}
                            {{ rowCount }} {{ $t('Pagination.entries') }}
                        </span>
                        <span v-else-if="currentPage === pageCount">
                            {{ $t('Pagination.Showing') }} {{
 (currentPage * 10) -
                            9
                            }}
                            {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }} {{ rowCount }} {{
                                $t('Pagination.entries')
                            }}
                        </span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="getPage()">
                            <b-pagination pills size="sm" v-model="currentPage" :total-rows="rowCount" :per-page="10"
                                          :first-text="$t('Table.First')" :prev-text="$t('Table.Previous')"
                                          :next-text="$t('Table.Next')" :last-text="$t('Table.Last')"></b-pagination>
                        </div>
                    </div>

                </div>
            </div>
            <loading :name="loading" v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>

        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from 'vue-multiselect'
import Loading from 'vue-loading-overlay';
import 'vue-loading-overlay/dist/css/index.css';
import moment from "moment";
export default {
    name: 'Customer',
    mixins: [clickMixin],
    components: {
        Multiselect,
        Loading
    },
    data: function () {
        return {
            isDisable: false,
            fromDate: '',
            toDate: '',
            randerforempty: 0,
            paymentTerms: '',
            paymentTerms2: '',
            b2b: false,
            b2c: false,
            advanceFilters: false,
            arabic: '',
            isCashCustomer: false,
            multipleAddress: false,
            active: false,
            english: '',
            customerlist: [],
            search: '',
            customer: [],
            loading: false,
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            limitedCustomer: 0,
            customerId: '',
            customerGroupId: '',
            category: '',
            customerCategory: '',
            customerListView : [],
            supplierListView : [],
        }
    },
    computed: {
            isFilterButtonDisabled() {
      return (
        this.customerId ||
        this.paymentTerms2 ||
        this.customerCategory ||
        this.customerGroupId ||
        this.category 
      );
    },
  },
    watch: {
        // search: function (val) {
        //     this.GetCustomerData(val, 1, this.active);
        // }
    },
    methods: {

        makeActive: function (item) {

            this.active = item;
            this.GetCustomerData(this.search, 1, item);
        },
        // isCashCustomerFunc: function () {

        //     if (this.isCashCustomer) {

        //         this.customerType = 'Individual';
        //         this.paymentTerms = 'Cash';

        //     } else {
        //         this.customerType = '';
        //         this.paymentTerms = 'Credit';
        //     }
        // },
        AdvanceFilterFor: function () {

            this.advanceFilters = !this.advanceFilters;
            if (this.advanceFilters == false) {
                this.FilterRecord(false);
            }

        },
        FilterRecord: function (type) {

            if (type) {
                if (this.fromDate != '') {
                    if (this.toDate == '') {
                        this.$swal({
                            title: 'Error',
                            text: "Please Select To Date ",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        return;

                    }
                }
                if (this.toDate != '') {
                    if (this.fromDate == '') {
                        this.$swal({
                            title: 'Error',
                            text: "Please Select From Date ",
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 2000,
                            timerProgressBar: true,
                        });

                        return;

                    }
                }



            } else {
                this.isDisable = false;

                if (this.$refs.userDropdown != null) {
                    this.$refs.userDropdown.EmptyRecord();
                }


                this.fromDate = '';
                this.toDate = '';
                this.search = "";
                this.randerforempty++;
                this.customerId = '';
                this.paymentTerms2='';
                this.customerCategory='';
                this.category='';

                if (this.$refs.CustomerDropdown != undefined) {
                    this.$refs.CustomerDropdown.Remove();
                }


                if (this.$refs.CustomerGroupDropdown != undefined && this.$refs.CustomerGroupDropdown != null) {
                    this.$refs.CustomerGroupDropdown.EmptyRecord();
                }

            }

            this.GetCustomerData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.isCashCustomer, this.customerId, this.customerCategory,this.customerGroupId,this.category,this.paymentTerms,this.paymentTerms2);


        },
        search22: function () {
                this.GetCustomerData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.isCashCustomer, this.customerId, this.customerCategory,this.customerGroupId,this.category,this.paymentTerms2);
        },
        clearData: function () {
                this.search="";
                this.GetCustomerData(this.search, this.currentPage, this.active, this.fromDate, this.toDate, this.fromTime, this.toTime, this.isCashCustomer, this.customerId, this.customerCategory,this.customerGroupId,this.category,this.paymentTerms2);

        },
        getDate: function (date) {
            if (date == null || date == undefined) {
                return "";
            }
            else {
                return moment(date).format('LLL');
            }
        },
        GetDate1: function () {

            if (this.fromDate != '' || this.toDate != '') {
                this.isDisableMonth = true;
                this.year = '';
                this.month = '';
                this.monthObj = '';

            } else {
                this.isDisableMonth = false;
            }

        },
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        ImportDataFromXlsx: function () {
            var root = this;
            root.$router.push({
                path: '/ImportExportRecords',
                query: { data: 'Customer', }
            })
        },
        AddCustomer: function () {

            this.$router.push({
                    path: '/addCustomer',
                    query: {
                        formName: 'Customer',

                    }
                })


           
        },
        EditCustomer: function (Id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            var multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;

            this.$https.get('/Contact/ContactDetail?Id=' + Id + '&multipleAddress=' + multipleAddress, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.customer = response.data
                    root.$store.GetEditObj(root.customer);
                }
                root.$router.push({
                    path: '/addCustomer',
                    query: {
                        formName: 'Customer',
                        data: '',
                        Add: false,
                    }
                })
            });
        },
        getPage: function () {
            this.GetCustomerData(this.search, this.currentPage, this.active);
        },
        GetCustomerData: function (search, currentPage, isCashCustomer) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Contact/ContactList?isCustomer=true' + '&searchTerm=' + search + '&pageNumber=' + currentPage + '&isCashCustomer=' + isCashCustomer +'&CustomerId=' + this.customerId +'&customerGroupId=' + this.customerGroupId + '&customerCategory=' + this.customerCategory + '&category=' + this.category + '&paymentTerms2=' + this.paymentTerms2, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {


                    // root.$store.state.customerlist.push(response.data.contacts)
                    root.customerlist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;

                }
            });
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
                
                root.$https.get('/Contact/GetContactListExcel?IsDropDown=' + true + '&isCustomer=' + true + '&isActive=' + true + '&paymentTerms=' + paymentTerms + '&isCashCustomer=' + this.isCashCustomer + '&multipleAddress=' + multipleAddress, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'Customer Report.xlsx');
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
        RemoveCustomer: function (id) {
            var root = this;
            this.$swal({
                icon: 'error',
                title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure?' : 'هل أنت متأكد؟',
                text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will not be able to recover this!' : 'لن تتمكن من استرداد هذا!',
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                closeOnConfirm: false,
                closeOnCancel: false
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
                                root.$store.state.customerlist.splice(root.$store.state.customerlist.findIndex(function (i) {
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

        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.limitedCustomer = parseInt(localStorage.getItem('LimitedCustomer'));
        if (isNaN(this.limitedCustomer)) {
            this.limitedCustomer = 0
        }
        this.isCashCustomer = localStorage.getItem('CashCustomer') == 'true' ? true : false;
        this.multipleAddress = localStorage.getItem('MultipleAddress') == 'true' ? true : false;
        this.GetCustomerData(this.search, 1, this.active);
        this.GetItemViewSetup();
    }
}
</script>