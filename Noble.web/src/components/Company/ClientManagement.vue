<template>
    <div class="row">
        <div class="col-lg-12 col-sm-12 ">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">List Of Companies</h4>

                            </div>
                            <div class="col-auto align-self-center">

                                <a href="javascript:void(0)" class="btn btn-outline-primary mx-1 "
                                    v-on:click="AddCompany"><i class="fa fa-plus"></i> Add Company</a>
                                <a href="javascript:void(0)" class="btn btn-outline-primary mx-1 "
                                    v-on:click="GetCompanyInformation"> Push Record</a>
                                <a href="javascript:void(0)" class="btn btn-outline-primary mx-1 "
                                    v-on:click="GetWhiteLabelingInformation(true)"> Apply WhiteLabeling</a>
                                <a href="javascript:void(0)" class="btn btn-outline-primary mx-1 "
                                    v-on:click="GetWhiteLabelingInformation(false)"> Apply Color Sheet</a>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card ">

                <div class="card-body">
                    <div class="row">

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <div v-for="(company, index) in companies" :key="index + 3">

                                <div class="accordion" role="tablist">
                                    <b-card no-body class="mb-1">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <table class="table table-striped table-hover table_list_bg"
                                                style="margin:0;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width:3%">
                                                            {{ index + 1 }}
                                                        </td>
                                                        <td style="width: 23%; text-align: left">
                                                            <strong>

                                                                <a href="javascript:void(0)" v-b-toggle.accordion-1
                                                                    v-on:click="makeActiveCollapse(index, company.id)"
                                                                    style="color: rgb(6 60 175)">{{ company.nameEnglish }}</a>

                                                            </strong>
                                                            <div>
                                                                <strong>
                                                                    {{ company.nameArabic }}
                                                                </strong>

                                                            </div>
                                                        </td>

                                                        <td style="width: 23%; text-align: right">

                                                            CR({{ company.companyRegNo }})
                                                            <div>
                                                                Vat({{ company.vatRegistrationNo }})
                                                            </div>
                                                        </td>

                                                        <td style="width: 15%; text-align: left;padding-left:8px;">
                                                            Ph({{ company.phoneNo }})
                                                        </td>
                                                        <td style="width: 15%; text-align: left">
                                                            Client NO({{ company.clientNo }})
                                                        </td>
                                                        <td style="width: 20%; text-align: right">
                                                            <!--<button type="button" class="btn btn-primary " style="background-color: #219653; border-color: #219653" v-on:click="EditCompany(company.id)"> <i class="fa fa-edit"></i> Edit Record </button>-->
                                                            <button type="button" class="btn btn-sm btn-primary "
                                                                v-on:click="AddBusiness(company.id)"> <i
                                                                    class="fa fa-plus"></i> Bus </button>
                                                            <!--<button type="button" class="btn btn-sm btn-primary " style="background-color: #219653; border-color: #219653" v-on:click="GetCompanyInformation(company.id)"> Push Data</button>-->

                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!--<a href="javascript:void(0)" v-b-toggle.accordion-1 v-on:click="makeActiveCollapse(index)">{{company.nameEnglish}}</a>-->
                                        </b-card-header>

                                        <b-collapse id="accordion-1" accordion="my-accordion" role="tabpanel"
                                            v-if="index == collpase">
                                            <b-card-body style="padding-right:0 !important">
                                                <div v-for="(business, busIndex) in businesses" :key="busIndex + 3">
                                                    <div class="accordionchild" role="tablist">
                                                        <b-card no-body class="mb-1">
                                                            <b-card-header header-tag="header" class="p-1" role="tab">
                                                                <table
                                                                    class="table table-striped table-hover table_list_bg"
                                                                    style="margin:0;">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width:4%">
                                                                                {{ (index + 1) + '.' + (busIndex + 1) }}
                                                                            </td>
                                                                            <td style="width:25%; text-align:left">
                                                                                <strong>

                                                                                    <a href="javascript:void(0)"
                                                                                        v-b-toggle.accordion-11
                                                                                        v-on:click="makeActiveBusCollapse(busIndex, business.id)"
                                                                                        style="color: rgb(6 60 175)">{{ business.nameEnglish }}</a>
                                                                                </strong>
                                                                                <div>
                                                                                    <strong>
                                                                                        {{ business.nameArabic }}
                                                                                    </strong>
                                                                                </div>
                                                                            </td>

                                                                            <td style="width: 20%; text-align: left">
                                                                                {{ business.categoryInEnglish }}
                                                                                <div>
                                                                                    {{ business.categoryInArabic }}
                                                                                </div>


                                                                            </td>

                                                                            <td style="width: 25%; text-align: left">
                                                                                {{ business.addressEnglish }}
                                                                                <div>
                                                                                    {{ business.addressArabic }}
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 30%; text-align: right">
                                                                                <button type="button"
                                                                                    class="btn btn-sm btn-primary me-2 "
                                                                                    v-on:click="PrefixRecord(business.id, business.clientParentId,false)">
                                                                                    Branch Prefixes
                                                                                </button>
                                                                               
                                                                                <button type="button"
                                                                                    class="btn btn-sm btn-primary "
                                                                                    v-on:click="Addlocation(business.id, business.clientParentId)"><i
                                                                                        class="fa fa-plus"></i> Add Loc
                                                                                </button>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </b-card-header>
                                                            <b-collapse id="accordion-11" accordion="my-accordionchild"
                                                                role="tabpanel" v-if="busIndex == busCollapse">
                                                                <b-card-body style="padding-right:0 !important">
                                                                    <div v-for="(location, locIndex) in locations"
                                                                        :key="locIndex + 3">
                                                                        <div class="accordionInnerchild" role="tablist">
                                                                            <b-card no-body class="mb-1">
                                                                                <b-card-header header-tag="header"
                                                                                    class="p-1" role="tab">
                                                                                    <table
                                                                                        class="table table-striped table-hover table_list_bg"
                                                                                        style="margin:0;">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 5%">
                                                                                                    {{ (index + 1) + '.'
                                                                                                            + (busIndex + 1) + '.'
                                                                                                            + (locIndex + 1)
                                                                                                    }}
                                                                                                </td>
                                                                                                <td v-if="location.nobleGroupId != null"
                                                                                                    style="width: 15%; text-align: left">
                                                                                                    <strong>

                                                                                                        <a href="javascript:void(0)"
                                                                                                            v-on:click="EditCompanyGroup(location.id)">{{ location.nameEnglish }}</a>
                                                                                                    </strong>
                                                                                                    <div>
                                                                                                        <strong>
                                                                                                            {{ location.nameArabic }}
                                                                                                        </strong>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td v-else
                                                                                                    style="width: 15%; text-align: left">
                                                                                                    <strong>

                                                                                                        {{ location.nameEnglish }}
                                                                                                    </strong>
                                                                                                    <div>
                                                                                                        <strong>
                                                                                                            {{ location.nameArabic }}
                                                                                                        </strong>
                                                                                                    </div>
                                                                                                </td>

                                                                                                <td class="text-center"
                                                                                                    style="width: 10%; text-align: left">
                                                                                                    Ph({{ location.phoneNo }})
                                                                                                </td>
                                                                                                <td class="text-center"
                                                                                                    style="width: 30%; text-align: left">
                                                                                                    {{ location.addressEnglish }}
                                                                                                    <div>
                                                                                                        {{ location.addressArabic }}
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center"
                                                                                                    style="width: 10%; text-align: left">
                                                                                                    {{ location.groupName }}
                                                                                                    <div>
                                                                                                        {{ location.licenseType }}
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center"
                                                                                                    style="width: 10%; text-align: left"
                                                                                                    v-if="location.technicalSupportPeriod != 'UnLimited' && !location.isTechnicalSupport">
                                                                                                    {{ location.endDate }}
                                                                                                    <div>
                                                                                                        (End Date)
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center"
                                                                                                    style="width: 10%; text-align: left"
                                                                                                    v-else-if="location.technicalSupportPeriod != 'UnLimited' && location.isTechnicalSupport">
                                                                                                    {{ location.endDate }}
                                                                                                    <div>
                                                                                                        (Technical End
                                                                                                        Date)
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center"
                                                                                                    style="width: 10%; text-align: left"
                                                                                                    v-else>
                                                                                                    -
                                                                                                </td>
                                                                                                <td class="text-center" style="width: 15%">
                                                                                                    <button type="button"
                                                                                                        class="btn btn-sm btn-primary me-2 "
                                                                                                        v-on:click="PrefixRecord(location.id, location.businessParentId,true,location)">
                                                                                                        Branch List
                                                                                                    </button>
                                                                                                    <button type="button"
                                                                                                            class="btn btn-sm btn-primary  "
                                                                                                            v-on:click="SaveBranch(location.id, location.businessParentId)"> Add Branch</button>
                                                                                                   
                                                                                                </td>
                                                                                                

                                                                                                <td class="text-center" style="width: 10%">
                                                                                                   
                                                                                                    <div class="button-items">
                                                                                                        <button type="button" class="btn btn-soft-primary dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">Actions <i class="mdi mdi-chevron-down"></i></button>
                                                                                                        <div class="dropdown-menu">
                                                                                                            <a class="dropdown-item" href="javascript:void(0);" v-on:click="syncSetup(location.id)">Sync Setup </a>
                                                                                                            <a class="dropdown-item" href="javascript:void(0);" v-on:click="getWareHouse(location.id)">View WareHouse</a>
                                                                                                            <a class="dropdown-item"  href="javascript:void(0);" v-on:click="getTerminal(location.id)">View Terminal</a>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <!--<b-button block v-b-toggle.accordion-111 variant="primary" v-on:click="makeActiveLocCollapse(locIndex)">{{location}}</b-button>-->
                                                                                </b-card-header>

                                                                            </b-card>

                                                                        </div>
                                                                    </div>

                                                                </b-card-body>
                                                            </b-collapse>
                                                        </b-card>

                                                    </div>
                                                </div>


                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>

                                </div>

                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
        <addbranches :newbranch="newBranch"
                         :show="addBranch"
                         v-if="addBranch"
                         @close="addBranch=false"
                         :type="type" />
        <branchPrefixModel 
                        :show="branchPrefix"
                        :locationId="locationId"
                        :businessId="businessId"
                        v-if="branchPrefix"
                        @close="branchPrefix=false" />
        <BranchPrefixesList 
                        :show="branchlist"
                        :locationName="locationName"
                        :locationId="locationId"
                        :businessId="businessId"

                        v-if="branchlist"
                        @closeprefix="branchlist=false" />
        <addbranches :newbranch="newBranch"
                         :show="showBranch"
                         v-if="showBranch"
                         @close="showBranch=false"
                         :type="type" />
        <ftp-account-detail :show="showFtpDetail" :newftpdetail="newFtpDetail" v-if="showFtpDetail"
            @close="showFtpDetail = false" />
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
</template>


<script>
import axios from 'axios'

    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

export default {
    components: {
        Loading
    },
    data: function () {
        return {
            locationId: '',
            businessId: '',

            newBranch: {
                    id: '',
                    code: '',
                    branchName: '',
                    contactNo: '',
                    address: '',
                    city: '',
                    state: '',
                    postalCode: '',
                    country: '',
                    isActive: true,
                    isOnline: false,
                    isApproval: false,
                    isCentralized: false,
                    branchType: '',

                },
            newFtpDetail: {
                id: '00000000-0000-0000-0000-000000000000',
                host: '',
                port: '',
                username: '',
                password: '',
                isWhiteLabbeling: false,
            },
            showFtpDetail: false,
            addBranch: false,
            branchPrefix: false,
            branchlist: false,
            locationName: '',
            showBranch: false,

            searchQuery: '',
            show: false,
            list: false,
            loading: false,
            companyList: [],
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            companyLicenceList: [],
            newPaymentLimit: {
                id: '00000000-0000-0000-0000-000000000000',
                fromDate: '',
                toDate: '',
                message: '',
                isActive: '',
                companyId: '00000000-0000-0000-0000-000000000000'
            },
            showHistory: false,
            showPaymentLimit: false,
            newLicense: {
                nobleGroupId: '',
                fromDate: '',
                toDate: '',
                isActive: false,
                isBlock: false,
                companyId: '',
                licenseType: '',
                gracePeriod: false,
                paymentFrequency: '',
                isTechnicalSupport: false,
                isUpdateTechnicalSupport: false,
                technicalSupportPeriod: '',
                activationPlatform: ''
            },
            companies: [],
            businesses: [],
            locations: [],
            collpase: '',
            busCollapse: '',
        }
    },
    watch: {
        search: function (val) {
            this.GetCompanyData(val, 1);
        }
    },
    methods: {
        syncSetup: function (companyId) {
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }

            this.$https.get('/System/SyncSetup?companyId=' + companyId, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    console.log(response.data);
                })
        },
        getTerminal: function (id) {

            var option = ''
            //this.companyOptionList.forEach(function (x) {
            //    if (x.locationId == id && x.label == 'overWrite')
            //        option = x.value
            //});
            //console.log(option)
            this.$router.push({
                path: '/terminal',
                query: {
                    id: id,
                    option: option
                }
            })
        },
        getWareHouse: function (id) {
            this.$router.push({
                path: '/warehouse',
                query: {
                    id: id,
                    Add: true,
                }
            })
        },
        SaveBranch: function (locationId,businessId) {
            this.newBranch = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    branchName: '',
                    contactNo: '',
                    address: '',
                    city: '',
                    state: '',
                    postalCode: '',
                    country: '',
                    branchType: '',
                    isActive: true,
                    locationId: locationId,
                    businessId: businessId,
                    isOnline: false,
                    isApproval: false,
                    isCentralized: false,
                }


                this.addBranch = !this.addBranch;
                this.type = "Add";
        },
        PrefixRecord: function (locationId,businessId,list,locationObj) {
            
            if(list)
            {
            this.locationId =locationId;
            this.businessId =businessId;
            this.branchlist=!this.branchlist;
            this.locationName=locationObj.nameEnglish;

            }
            else
            {
            this.businessId =locationId;
            // this.locationId =locationId;
            this.branchPrefix =!this.branchPrefix;

            }
          

          
            },
        BranchRecord: function (id) {
            this.newBranch = {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    branchName: '',
                    locationId: id,
                    contactNo: '',
                    address: '',
                    city: '',
                    state: '',
                    postalCode: '',
                    country: '',
                    branchType: '',

                }

                this.showBranch = !this.showBranch;
                this.type = "Add";
        },
        AddCompany: function () {
            // this.$router.push('/AddCompany')
             this.$router.push({
                path: '/AddCompany',
                query: {
                    Add: true,
                    isDeliveryChallan: true,

                }
            })
        },
        EditCompany: function (Id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Company/EditCompany?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.companylist = response.data
                }
                root.$store.GetEditObj(response.data);
                root.$router.push({
                    path: '/AddCompany',
                    query: {
                        data: '',
                        Add: false,
                    }
                })
            });
        },
        AddBusiness: function (companyId) {
            this.$router.push({
                path: '/addbusiness',
                query: { 
                    data: companyId,
                    Add: true, 
                }
            })
        },
       
        Addlocation: function (busId, clientParentId) {

            this.$router.push({
                path: '/addlocation',
                query: 
                { 
                    busId: busId, 
                    Add: true,
                    clientParentId: clientParentId 
                }
            })
        },
        GetCompanyInformation: function () {
            var root = this;

            root.loading = true;
            this.$https.get('/Company/GetCompanyInformationForPermission').then(function (response) {
                if (response.data != null) {
                    if (response.data.isSuccess) {
                        axios.post(root.$PermissionIp + '/NoblePermission/CompanyInformation', response.data.message).then(function (res) {
                            if (res.data != null) {
                                if (res.data.isSuccess && res.data.message === 'Updated') {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                        text: "Push Record Updated!",
                                        type: 'success',
                                        icon: 'success',
                                        showConfirmButton: false,
                                        timer: 1500,
                                        timerProgressBar: true,
                                    });
                                }
                                else if (res.data.isSuccess && res.data.message === 'Success') {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                        text: "Push Record Added!",
                                        type: 'success',
                                        icon: 'success',
                                        showConfirmButton: false,
                                        timer: 1500,
                                        timerProgressBar: true,
                                    });
                                }
                                else {
                                    root.$swal.fire(
                                        {
                                            icon: 'error',
                                            title: 'Something Went Wrong to push record!',
                                            text: res.data.message,

                                            showConfirmButton: false,
                                            timer: 5000,
                                            timerProgressBar: true,
                                        });
                                }
                            }
                            root.loading = false;
                        }).catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: 'Something Went Wrong to push record!',
                                    text: error.response.data,

                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });
                            root.loading = false;

                        });
                    }

                }

            }).catch(error => {
                console.log(error)
                root.$swal.fire(
                    {
                        icon: 'error',
                        title: 'Something Went Wrong to fetch record!',
                        text: error.response.data,

                        showConfirmButton: false,
                        timer: 5000,
                        timerProgressBar: true,
                    });
                root.loading = false;

            });
        },
        makeActiveCollapse: function (item, companyId) {

            this.collpase = item;
            this.businesses = []
            var root = this;
            this.companyList.businesses.forEach(function (x) {

                if (x.clientParentId === companyId) {
                    root.businesses.push(x)
                }
            })
            //this.businesses.push(.find(x => x.clientParentId == companyId))
        },
        makeActiveBusCollapse: function (item, busId) {
            this.busCollapse = item;
            this.locations = []
            var root = this;
            this.businessId=busId;
            this.companyList.locations.forEach(function (x) {

                if (x.businessParentId === busId) {
                    root.locations.push(x)
                }
            })
        },


        getPage: function () {
            this.GetCompanyData(this.search, this.currentPage);
        },

        GetCompanyData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            this.$https.get('/Company/GetCompanyList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    console.log(response.data.message)
                    root.companies = response.data.message.companies
                    root.companyList = response.data.message
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        showLicenceHistory: function (name, licenceList) {

            this.companyLicenceList = [];
            this.companyName = name;
            this.showHistory = !this.showHistory;
            this.companyLicenceList = licenceList;
        },
        GetWhiteLabelingInformation: function (isWhitLabelling) {
            this.newFtpDetail.isWhiteLabbeling = isWhitLabelling;
            this.showFtpDetail = !this.showFtpDetail;

        },

    },
    created: function () {
        localStorage.setItem('locales', 'en');
        this.$emit('update:modelValue', this.$route.name);
    },
    mounted: function () {
        this.GetCompanyData();

    }
}</script>