<template>
    <div class="ListData">
        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
                <div class="card ">
                    <div class="card-header">
                        <div class="row ml-1">
                            <h4 class="card-title DayHeading">{{ $t('Business.Business') }}</h4>
                        </div>
                        <div class="row">
                            <div class="col-md-5 col-lg-5">
                                <div class="form-group">
                                    <label></label>
                                    <input type="text" class="form-control" v-model="searchQuery" name="search" id="search" placeholder="Search by Name, Bussiness Type" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <div class="col-lg-12">
                            <div class="row">
                                <div>
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="AddBusiness"><i class="fa fa-plus"></i>{{ $t('Business.AddBusiness') }}</a>
                                </div>
                            </div>
                            <div class="mt-2">
                                <div class=" table-responsive">
                                    <table class="table ">
                                        <thead class="m-0">
                                            <tr>
                                                <th>#</th>
                                                <th>
                                                    {{ $t('Business.NameEn') }}
                                                </th>
                                                <th>
                                                    {{ $t('Business.NameArabic') }}
                                                </th>
                                                <th>
                                                    {{ $t('Business.BusinessTypeEng') }}
                                                </th>

                                                <th>
                                                    {{ $t('Business.BusinessTypeArabic') }}
                                                </th>


                                                <!--<th>
                                                    {{ $t('Company.Action') }}
                                                </th>-->
                                                <!--<th>{{ $t('Permission') }}</th>-->
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(business,index)  in resultQuery" v-bind:key="business.id">
                                                <td>
                                                    {{index+1}}
                                                </td>
                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)" v-on:click="EditBusiness(business.id)">{{business.nameEnglish}}</a>
                                                    </strong>
                                                </td>

                                                <td>{{business.nameArabic}}</td>
                                                <td>{{business.categoryEnglish}}</td>
                                                <td>{{business.categoryArabic}}</td>
                                                <!--<td>
                                                    <strong>
                                                        <button type="button" class="btn btn-primary  " v-on:click="AddLicence(business.id,business.nameEnglish, business.companyLicenceId, business.companyLicences)">
                                                            <span v-if="business.companyType == null || business.companyType == '' || business.companyType == undefined">
                                                                {{ $t('Company.AddLicense') }}
                                                            </span>
                                                            <span v-else>
                                                                {{ $t('Company.UpdateLicense') }}
                                                            </span>
                                                        </button>
                                                    </strong>
                                                    <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="showLicenceHistory(business.nameEnglish, business.companyLicences)" title="Show Licence History"><i class=" fa fa-history"></i></a>
                                                </td>-->
                                                <!--<td>
                                                    <strong>
                                                        <button type="button" class="btn btn-primary  " v-on:click="AddPermissions(business.id)">
                                                            <span>
                                                                {{ $t('Add') }}
                                                            </span>
                                                        </button>
                                                    </strong>
                                                    <strong>
                                                        <button type="button" class="btn btn-primary  " v-on:click="UpdatePermissions(business.id)">
                                                            <span>
                                                                {{ $t('Update') }}
                                                            </span>
                                                        </button>
                                                    </strong>
                                                </td>-->
                                                <!--<a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="RemoveCompany(company.id)"><i class=" fa fa-trash"></i></a>-->
                                                <!--<td><a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="RemoveBusiness(business.id)"><i class=" fa fa-trash"></i></a></td>-->
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <licence-model :show="show"
                           :companyId="companyId"
                           :companyName="companyName"
                           :companyLicenceId="companyLicenceId"
                           :companyLicenceList="companyLicenceList"
                           v-if="show"
                           @close="closeModal"
                           :type="type" />

            <licence-history-model :show="showHistory"
                                   :companyName="companyName"
                                   :companyLicenceList="companyLicenceList"
                                   v-if="showHistory"
                                   @close="showHistory = false" />
            <assignPermissionsToRoleForAdd :roledetailsforpermissions="roleDetailsForPermissions"
                                           :show="showPermissions"
                                           v-if="showPermissions"
                                           @close="showPermissions = false"
                                           :type="typePermissions" />

            <updatePermissionsToRole :roleDetails="roleDetailsForPermissions"
                                     :show="updatePermissions"
                                     v-if="updatePermissions"
                                     @close="updatePermissions = false"
                                     :type="typePermissions" />
        </div>
    </div>
</template>
<script>


    export default {
        name: 'business',
        data: function () {
            return {
                businesslist: [],
                searchQuery: '',
                show: false,
                type: '',
                companyId: null,
                companyName: '',
                companyLicenceId: '',
                companyLicenceList: [],
                showHistory: false,
                roleDetailsForPermissions: [],
                typePermissions: '',
                showPermissions: false,
                updatePermissions: false
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return root.businesslist.filter((businesslist) => {

                        return root.searchQuery.toLowerCase().split(' ').every(v => businesslist.categoryArabic.toLowerCase().includes(v) || businesslist.categoryEnglish.toLowerCase().includes(v) || businesslist.nameEnglish.toLowerCase().includes(v))
                    })
                } else {
                    return root.businesslist;
                }
            },
        },

        methods: {
            AddPermissions: function (id) {
                this.roleDetailsForPermissions = {
                    roleId: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    allowAll: false,
                    removeAll: false,
                    moduleId: '',
                    companyId: id,
                    id: '00000000-0000-0000-0000-000000000000'
                }
                this.showPermissions = !this.showPermissions;
                this.typePermissions = "Add";
            },
            UpdatePermissions: function (id) {
                this.roleDetailsForPermissions = {
                    roleId: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    allowAll: false,
                    removeAll: false,
                    moduleId: '',
                    companyId: id,
                    id: '00000000-0000-0000-0000-000000000000'
                }
                this.updatePermissions = !this.updatePermissions;
                this.typePermissions = "Add";
            },
            closeModal: function () {
                this.show = false;
                this.GetBusinessData();
            },
            AddLicence: function (id, name, companyLicenceId, licenceList) {
                this.companyLicenceList = [];
                this.companyName = name;
                this.companyId = id;
                this.companyLicenceId = companyLicenceId;
                this.show = !this.show;
                this.companyLicenceList = licenceList;
                this.type = "Add";
            },
            showLicenceHistory: function (name, licenceList) {
                this.companyLicenceList = [];
                this.companyName = name;
                this.showHistory = !this.showHistory;
                this.companyLicenceList = licenceList;
            },
            GetBusinessData: function () {

                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/BusinessList', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.businesslist = response.data;
                    }
                });
            },
            RemoveBusiness: function (id) {
                var root = this;
                // working with IE and Chrome both
                this.$swal({
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
                        if (token == '') {
                            token = localStorage.getItem('token');
                        }
                        this.$https.post('/Company/Delete?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                            .then(function (response) {
                                if (response.data) {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Deleted!' : 'تم الحذف!',
                                        text: 'Comapany has been Deleted.',
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });
                                } else {
                                    console.log("error: something wrong from db.");
                                }
                            },
                                function (error) {
                                    root.loading = false;
                                    console.log(error);
                                });
                    }
                    else {
                        this.$swal((this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Cancelled!' : 'ألغيت!', (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Your file is still intact!' : 'ملفك لا يزال سليما!', (this.$i18n.locale == 'en' || root.isLeftToRight()) ? 'info' : 'معلومات');
                    }
                });
            },
            AddBusiness: function () {
                // this.$router.push('/addbusiness')
                 this.$router.push({
                path: '/addbusiness',
                query: {
                    Add: true,
                    isDeliveryChallan: true,

                }
            })
            },
        },
        mounted: function () {

            this.GetBusinessData();
        }
    }
</script>