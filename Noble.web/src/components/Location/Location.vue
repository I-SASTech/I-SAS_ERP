<template>
    <div class="ListData">
        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
                <div class="card ">
                    <div class="card-header">
                        <div class="row ml-1">
                            <h4 class="card-title DayHeading">{{ $t('Location.Location') }}</h4>
                        </div>
                        <div class="row">
                            <div class="col-md-4 m-1 " v-bind:class="{'has-danger' : v$.business.List.$error}">
                                <label></label>
                                <input type="text" class="form-control" v-model="searchQuery" name="search" id="search" placeholder="Search by Name, VAT, Reg, Mobile No" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="card">
                    <div class="card-body">
                        <div class="col-lg-12">
                            <div class="row">
                                <div>
                                    <a href="javascript:void(0)" class="btn btn-outline-primary " v-on:click="AddLocation"><i class="fa fa-plus"></i> Add</a>
                                </div>
                            </div>
                            <div class="mt-2">
                                <div class=" table-responsive">
                                    <table class="table ">
                                        <thead class="m-0">
                                            <tr>
                                                <th>#</th>
                                                <th>
                                                    {{ $t('Location.Name') }}
                                                </th>
                                                <th>
                                                    {{ $t('Location.Mobile') }}
                                                </th>
                                                <th>
                                                    {{ $t('Location.RegNo') }}
                                                </th>
                                                <th>
                                                    {{ $t('Location.VatNo') }}
                                                </th>
                                                <th>
                                                    {{ $t('Location.Email') }}
                                                </th>
                                                <th>
                                                    {{ $t('Location.Website') }}
                                                </th>
                                                <!--<th>
                                                    {{ $t('Company.Action') }}
                                                </th>-->
                                                <!--<th>  {{ $t('Permission') }}</th>-->
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(details, index) in resultQuery" v-bind:key="details.id">
                                                <td>
                                                    {{index+1}}
                                                </td>
                                                <td>
                                                    <strong>
                                                        <a href="javascript:void(0)">{{details.nameEnglish}}</a>
                                                    </strong>
                                                </td>
                                                <td>{{details.companyRegNo}}</td>
                                                <td>{{details.vatRegistrationNo}}</td>
                                                <td>{{details.phoneNo}}</td>
                                                <td>{{details.email}}</td>
                                                <td>{{details.website}}</td>
                                                <!--<td>
                                                    <strong>
                                                        <button type="button" class="btn btn-primary  " v-on:click="AddLicence(details.id,details.nameEnglish, details.companyLicenceId, details.companyLicences)">
                                                            <span v-if="details.companyType == null || details.companyType == '' || details.companyType == undefined">
                                                                {{ $t('Company.AddLicense') }}
                                                            </span>
                                                            <span v-else>
                                                                {{ $t('Company.UpdateLicense') }}
                                                            </span>
                                                        </button>
                                                    </strong>
                                                    <a href="javascript:void(0)" class="btn btn-danger btn-sm btn-icon " v-on:click="showLicenceHistory(details.nameEnglish, details.companyLicences)" title="Show Licence History"><i class=" fa fa-history"></i></a>
                                                </td>
                                                <td>
                                                    <strong>
                                                        <button type="button" class="btn btn-primary  " v-on:click="AddPermissions(details.id)">
                                                            <span>
                                                                {{ $t('Add') }}
                                                            </span>
                                                        </button>
                                                    </strong>
                                                    <strong>
                                                        <button type="button" class="btn btn-primary  " v-on:click="UpdatePermissions(details.id)">
                                                            <span>
                                                                {{ $t('Update') }}
                                                            </span>
                                                        </button>
                                                    </strong>
                                                </td>-->
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
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
</template>
<script>
    import useVuelidate from '@vuelidate/core'
    import { required, maxLength } from '@vuelidate/validators';
    export default {
        name: 'business',
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                show: false,
                type: '',
                searchQuery: '',
                business: {},
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
                    return root.business.filter((business) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => business.nameEnglish.toLowerCase().includes(v) || business.companyRegNo.toLowerCase().includes(v) || business.vatRegistrationNo.toLowerCase().includes(v) || business.phoneNo.toLowerCase().includes(v))
                    })
                } else {
                    return root.business;
                }
            },
        },
        validations() {
            return {
                business: {
                    List: {
                        required,
                        maxLength: maxLength(2)
                    },
                }
                }
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
                this.GetLocationData();
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
            AddLocation: function () {
                this.$router.push('/AddLocation')
            },
            GetLocationData: function () {
                var root = this;
                var url = '/Company/LocationList';
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.business = response.data;
                    }
                });
            },
        },
        mounted: function () {
            this.GetLocationData();
        }
    }
</script>