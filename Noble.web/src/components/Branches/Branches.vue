<template>
    <div class="row" >

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Branches.Branches') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Branches.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Branches.Branches') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                
                                <!-- <a  v-on:click="openmodel"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Branches.AddBranch') }}
                                </a> -->
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('Terminal.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1">
                            <i class="fas fa-search"></i>
                        </button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('Terminal.SearchbyName')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>

                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>
                                        {{ $t('Branches.Code') }}
                                    </th>
                                    <th>
                                        {{ $t('Branches.BranchName') }}
                                    </th>
                                    <th>
                                        Location Name
                                    </th>
                                    <th>
                                        Branch Type
                                    </th>
                                    <th>
                                        {{ $t('Branches.ContactNo') }}
                                    </th>
                                    <th>
                                        {{ $t('Branches.Address') }}
                                    </th>
                                    <th>
                                        {{ $t('Branches.City') }}
                                    </th>
                                    <th>
                                            Main Branch
                                        </th>
                                        <th>
                                            Centerlized
                                        </th>
                                        <th>
                                            Online
                                        </th>
                                        <th>
                                            Approval
                                        </th>
                                        <th>
                                           Status
                                        </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(terminal,index) in resultQuery" v-bind:key="index">
                                    <td>
                                        {{index+1}}
                                    </td>

                                  
                                    <td >
                                        {{terminal.code}}
                                    </td>
                                    <td>
                                        {{terminal.branchName}}
                                    </td>
                                    <td>
                                        {{terminal.locationName}}
                                    </td>
                                    <td>
                                        {{terminal.branchType}}
                                    </td>
                                    <td>
                                        {{terminal.contactNo}}
                                    </td>
                                    <td>
                                        {{terminal.address}}
                                    </td>
                                    <td>
                                        {{terminal.city}}
                                    </td>
                                    <td>
                                            <span v-if="terminal.mainBranch" class="badge badge-boxed  badge-outline-success">Main Branch</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger"></span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isCentralized" class="badge badge-boxed  badge-outline-success">Centerlized</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">De-Centerlized</span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isOnline" class="badge badge-boxed  badge-outline-success">Online</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">Offline</span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isApproval" class="badge badge-boxed  badge-outline-success">Approval</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">Without Approval</span>
                                        </td>
                                        <td>
                                            <span v-if="terminal.isActive" class="badge badge-boxed  badge-outline-success">{{$t('color.Active')}}</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">{{$t('color.De-Active')}}</span>
                                        </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />


                </div>
            </div>

            <addbranches :newbranch="newBranch"
                         :show="show"
                         v-if="show"
                         @close="GetBranch()"
                         :type="type" />
        </div>
    </div>
   
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                show: false,
                terminallist: [],
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
                },
                type: '',
                searchQuery: '',
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.terminallist.filter((terminal) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => terminal.code.toLowerCase().includes(v))
                    })
                } else {
                    return root.terminallist;
                }
            },
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            openmodel: function () {
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
                }

                this.show = !this.show;
                this.type = "Add";
            },

            GetBranch: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.show = false;

                root.$https.get('Branches/BranchList?isDropdown=true' + '&locationId=' + localStorage.getItem('CompanyID'), { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.terminallist = response.data.results;
                    }
                });
            },

            EditTerminal: function (id) {

                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Branches/GetBranchDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newBranch.id = response.data.id;
                            root.newBranch.code = response.data.code;
                            root.newBranch.branchName = response.data.branchName;
                            root.newBranch.contactNo = response.data.contactNo;
                            root.newBranch.address = response.data.address;
                            root.newBranch.city = response.data.city;
                            root.newBranch.state = response.data.state;
                            root.newBranch.postalCode = response.data.postalCode;
                            root.newBranch.country = response.data.country;

                            root.show = !root.show;
                            root.type = "Edit"
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            root.loading = false;
                            console.log(error);
                        });

            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
            this.GetBranch();
        },
        mounted: function () {

        }
    }
</script>