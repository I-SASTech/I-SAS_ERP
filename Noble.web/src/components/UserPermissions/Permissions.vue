<template>
    <div class="row" v-if="isValid('CanViewUserPermission')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col">
                    <h5 class="page_title">{{ $t('UpdatePermissionsToRole.Permissions') }}</h5>
                    <ol class="breadcrumb">
                        <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('UpdatePermissionsToRole.Home') }}</a></li>
                        <li class="breadcrumb-item active">
                            {{ $t('UpdatePermissionsToRole.Permissions') }}
                        </li>
                    </ol>
                </div>

                <div class="col-auto align-self-center">
                    <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                       class="btn btn-sm btn-outline-danger">
                        {{ $t('ThePermissions.Close') }}
                    </a>
                </div>
            </div>
        </div>
        <div class="col-sm-12 ml-auto mr-auto">
            <div class="card  ">
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th style="width: 60%">
                                        {{ $t('ThePermissions.RoleName') }}
                                    </th>
                                    <th>
                                        {{ $t('ThePermissions.UserAndPermissions') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(roles,index) in resultQuery" v-bind:key="roles.id">
                                    <td>
                                        {{index+1}}
                                    </td>

                                    <td style="width: 60%">
                                        <strong>
                                            <a href="javascript:void(0)">{{roles.name}}</a>
                                        </strong>
                                    </td>
                                    <td>
                                        <button class="btn btn-outline-primary  btn-icon mx-1" v-if="isValid('CanViewUserPermission')" @click="addRoleToUsers(roles.id, roles.name)" data-toggle="tooltip" data-placement="left" title="Assign User">
                                            <i class="far fa-user"></i>
                                        </button>
                                        <button class="btn btn-outline-info  btn-icon" v-if="isValid('CanViewUserPermission')" @click="addPermissionsToRoles(roles.id, roles.name)" data-toggle="tooltip" data-placement="right" title="Assign Permission">
                                            <i class="ti-unlock"></i>
                                        </button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <assignRoleToUsers :roleDetails="roleDetails"
                           :show="show"
                           v-if="show"
                           @close="show = false"
                           :type="type" />

        <assignPermissionsToRole :roleDetails="roleDetailsForPermissions"
                                 :show="showPermissions"
                                 v-if="showPermissions"
                                 @close="showPermissions = false"
                                 :type="typePermissions" />
    </div>
    <div v-else> <acessdenied></acessdenied></div>
</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                searchQuery: '',
                roleslist: [],
                roleDetails: [],
                roleDetailsForPermissions: [],
                type: '',
                show: false,
                typePermissions: '',
                showPermissions: false,
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.roleslist.filter((roles) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => roles.name.toLowerCase().includes(v))
                    })
                } else {
                    return root.roleslist;
                }
            },
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            addRoleToUsers: function (id, name) {
                this.roleDetails = {
                    roleId: id,
                    name: name,
                    userId: '',
                    id: '00000000-0000-0000-0000-000000000000'
                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetListOfPermission: function (roleId,name) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/GetListOfPermission?roleId=' + roleId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.$store.GetEditObj(response.data);
                        root.$router.push({ 
                            path: '/NoblePermissions', 
                            query: { 
                                data: '',
                                Add: false,
                                roleName:name } 
                            })
                    }
                });
            },
            addPermissionsToRoles: function (id, name) {
                this.GetListOfPermission(id,name);
               
                console.log(id)
                console.log(name)
                //this.roleDetailsForPermissions = {
                //    roleId: id,
                //    name: name,
                //    allowAll: false,
                //    removeAll: false,
                //    moduleId: '',
                //    id: '00000000-0000-0000-0000-000000000000'
                //}
                //this.showPermissions = !this.showPermissions;
                //this.typePermissions = "Add";
            },
            GetRolesData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.$store.GetRoleList(response.data.nobleRoleModel);
                        root.roleslist = response.data.nobleRoleModel;
                    }
                });
            }
        },
        created: function () {
            this.$emit('update:modelValue', this.$route.name);
        },
        mounted: function () {
            this.GetRolesData();
        }
    }
</script>