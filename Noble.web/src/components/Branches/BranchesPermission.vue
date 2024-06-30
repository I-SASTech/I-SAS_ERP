<template>
    <div class="row" >

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">Branches Permission</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Branches.Home') }}</a></li>
                                    <li class="breadcrumb-item active">Branches Permission</li>
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
               
                <div class="card-body">
                    <div class="row">
                        <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder">Branches:</label>
                                    <branch-dropdown :islayout="false" :ismultiple="false"  v-model="branchId" @update:modelValue="GetBranchRecord"></branch-dropdown>
                    </div>
                    <div class="form-group has-label col-sm-6 mt-3 text-end">
                        <button type="button" class="btn btn-primary mx-2" v-on:click="SavePermissions" > Sync Record</button>
                        <button type="button" class="btn btn-danger " v-on:click="close()">{{ $t('AddBranches.Cancel') }}</button>


                    </div>
                    </div>
                    <div class="row ">
                        <div class="form-group has-label col-12 ">
                            <div class="row">
                                <div class="accordion" id="accordionExample">
                                    <div class="accordion-item" v-for="(module,index) of noblePermission.modules" :key='module.name + 3'>
                                        <h5 class="accordion-header m-0" :id="'headingOne'+index">
                                            <button class="accordion-button fw-semibold collapsed " type="button" data-bs-toggle="collapse" :data-bs-target="'#collapseOne'+index" aria-expanded="false" :aria-controls="'collapseOne'+index" v-on:click="ShowOptions(module)">
                                                {{module.name}}
                                            </button>
                                        </h5>
                                        <div :id="'collapseOne'+index" class="accordion-collapse collapse" aria-labelledby="'headingOne'+index" data-bs-parent="#accordionExample">
                                            <div class="accordion-body">
                                                <div class="row">
                                                    <div class="col-12 ">
                                                        <h5>{{ $t('PermissonModules.Permissions') }} ({{moduleName}})</h5>
                                                    </div>
                                                    <div class="col-12 ">
                                                        <div class="checkbox">
                                                            <input v-model="isChecked" v-on:change="onClickCheckBox(moduleId)" id="checkbox0" type="checkbox">
                                                            <label for="checkbox0">
                                                                {{ $t('PermissonModules.SelectAllPermissionOfModule') }} 
                                                            </label>
                                                        </div>
                                                        <!--<input type="checkbox" v-model="isChecked" v-on:change="onClickCheckBox(moduleId)" /><span style="margin-left:5px;">Select All Permission Of Module</span>-->

                                                    </div>
                                                    <template v-for="perType of permissionType">
                                                        <div class="col-12" v-if="perType.moduleId === moduleId && noblePermission.permissions.filter(x => x.typeId === perType.id ).length > 0" :key='perType.value + 3'>

                                                            <h5>
                                                                {{perType.value}}:
                                                            </h5>
                                                            <div class="row">
                                                                <template v-for="per of noblePermission.permissions">
                                                                    <div class="col-4" v-if="per.moduleId === moduleId && per.typeId === perType.id" :key='per.value + 3'>
                                                                        <div class="checkbox">
                                                                            <input v-model="per.checked" v-on:change="updateAllCheckStatuc(moduleId)" :id="per.permissionName" type="checkbox">
                                                                            <label :for="per.permissionName">
                                                                                {{per.permissionName}}
                                                                            </label>
                                                                        </div>

                                                                    </div>
                                                                </template>

                                                            </div>
                                                            <hr />
                                                        </div>
                                                    </template>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                   
                    <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>

                   


                </div>
            </div>

        </div>
    </div>
   
</template>

<script>
  import modules from '@/enums/modules'
    import permissions from '@/enums/permissions'
    import permissionType from '@/enums/permissiontype'
    import clickMixin from '@/Mixins/clickMixin'
    
    
    export default {
        mixins: [clickMixin],
        components: {
            

        },
        data: function () {
            return {
                loading: false,
                terminallist: [],
                noblePermission: {
                    modules: [],
                    permissions: [],
                    groupId: '',
                },
                roleName: '',
                moduleId: '',
                moduleName: '',
                modules: modules,
                isChecked: false,
                branchId: '',
                permissions: permissions,

                permissionType: permissionType,
                groupList: ['ERP', 'Retail', 'WholeSale'],
                groupTypeList: ['Basic', 'Advance', 'Premium', 'Customize']
            }
        },
       
        methods: {
            GetBranchRecord: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.loading=true;

               
                root.$https.get('Company/GetBranchWisePermission?branchId=' +this.branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.loading=false;

                        root.terminallist = response.data.message;
                        if(response.data.message.length==0)
                        {
                            for (var index = 0; index < root.noblePermission.permissions.length; index++) {
                                root.noblePermission.permissions[index].checked = false;
                            }
                            root.loading=false;
                        }
                        else
                        {
                            response.data.message.forEach(function (x) {
                                var index = root.noblePermission.permissions.findIndex((y => y.key == x.key));
                                if (index >= 0) {
                                    root.noblePermission.permissions[index].checked = true;
                                }
                            });
                            root.loading=false;
                        }
                        
                    }
                });
            },
            onClickCheckBox: function (moduleId) {

                // var root = this;
                var checkPermissionUpdate = false
                this.noblePermission.modules.forEach(function (x) {
                    if (x.id == moduleId) {
                        x.checked = !x.checked
                        checkPermissionUpdate = x.checked
                    }
                })
                this.noblePermission.permissions.forEach(function (x) {
                    if (x.moduleId == moduleId && checkPermissionUpdate) {
                        x.checked = true
                    }
                    else if (x.moduleId == moduleId && !checkPermissionUpdate) {
                        x.checked = false
                    }
                })
            },
            GetSelectedtGroupData: function () {
                var root = this;
                this.ConvertEnumToList()
                if (this.noblePermission.groupId != "") {
                    this.$https.get('/NoblePermission/GetNoblePermissionByGroupId?id=' + this.noblePermission.groupId).then(function (response) {
                        if (response.data != null) {
                            response.data.result.forEach(function (x) {
                                var index = root.noblePermission.permissions.findIndex((y => y.key == x.key));
                                if (index >= 0) {
                                    root.noblePermission.permissions[index].checked = true;
                                }
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

                    });

                }
            },
            SavePermissions: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if(this.branchId=='' || this.branchId==null)
                {
                    
                    root.$swal.fire(
                        {
                            icon: 'error',
                            title:  'Branch Id is compulsory' ,
                            text:'Branch Id is compulsory' ,
                            showConfirmButton: false,
                            timer: 5000,
                            timerProgressBar: true,
                        });

                }
                
                // var branchList=this.branchId.map(x=>x.id);
                var branchPermissionModel = {
                    permissions: this.noblePermission.permissions,
                    branchId: this.branchId,
                    roleName: 'Admin',
                    
                }

                
                //noblePermission.groupId = '00000000-0000-0000-0000-000000000000'
                this.$https.post('/Company/UpdateUserPermissionBranches' , branchPermissionModel, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.$swal({
                            icon: 'success',
                            title: 'Saved Successfully!',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });


                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: response.data.message,
                            type: 'error',
                            icon: 'error',
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

                });
            },
            ShowOptions: function (module) {
                this.moduleId = module.id;
                this.moduleName = module.name;
                var root = this;

                var allPermissionSelected = root.noblePermission.permissions.findIndex((y => !y.checked && y.moduleId === module.id));
                if (allPermissionSelected < 0) {
                    var moduleIndex = root.noblePermission.modules.findIndex((y => y.id === module.id));
                    root.noblePermission.modules[moduleIndex].checked = true
                    root.isChecked = true
                }
                else {
                    root.isChecked = false
                }
                this.show = true
            },
            ConvertEnumToList: function () {
                this.noblePermission.modules = []
                this.noblePermission.permissions = []

                var root = this;
                for (let item in this.modules) {

                    var moduleData = root.nobleModuleList.findIndex((y => y.moduleId === root.modules[item].id))
                    if (moduleData >= 0) {
                        this.noblePermission.modules.push({
                            id: root.modules[item].id,
                            name: root.modules[item].value,
                            checked: false
                        });
                    }



                }
                for (let item in this.permissions) {
                    //if (this.permissions[item].key != '3d1f65f1-3f72-4898-a175-1b6ab42b2b9d' || this.permissions[item].key != '7dc50e60-d5a2-419a-b12a-200ac71d7cb6') {
                    var permissionData = this.noblePermissionList.findIndex((y => y.key === this.permissions[item].key))
                    if (permissionData >= 0) {
                        this.noblePermission.permissions.push({
                            permissionName: this.permissions[item].permissionName,
                            key: this.permissions[item].key,
                            value: this.permissions[item].value,
                            moduleId: this.permissions[item].moduleId,
                            typeId: this.permissions[item].typeId,
                            checked: false,
                            id: this.noblePermissionList[permissionData].id,
                            groupId: '00000000-0000-0000-0000-000000000000'
                        });
                    }
                    //}


                }
                this.show = false;
                this.loading = false;
            },
            SaveSync: function () {
            },
           
            GetListOfPermission: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.loading=true;
            
                
                root.$https.get('/Company/GetListOfPermissionUserRole', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    

                    if (response.data != null) {
                        root.nobleModuleList = response.data.nobleModuleLook;
                        root.noblePermissionList = response.data.noblePermissionLook;
                        root.roleName = response.roleName;
                        root.ConvertEnumToList();

                    }
                });
            },
            GetBranchDetailRecord: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
            
                
                root.$https.get('/Company/GetBranchWisePermission?branchId='+this.branchId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    

                    if (response.data != null) {
                        root.nobleModuleList = response.data.nobleModuleLook;
                        root.noblePermissionList = response.data.noblePermissionLook;
                        root.roleName = response.roleName;
                        root.ConvertEnumToList();

                    }
                });
            },
            

          
        },
        created: function () {
            this.GetListOfPermission();
        },
        mounted: function () {

        }
    }
</script>