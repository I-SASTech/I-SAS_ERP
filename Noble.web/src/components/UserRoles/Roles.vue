<template>
    <div class="row" v-if="isValid('CanViewUserRole')">        

        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Roles.Roles') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Roles.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Roles.Roles') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddUserRole')" v-on:click="openmodel" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Categories.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('Categories.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('Roles.SearchByName')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th v-if="english=='true'">
                                        {{$englishLanguage($t('Roles.NameEn'))  }}
                                    </th>
                                    <th v-if="isOtherLang()">
                                        {{$arabicLanguage($t('Roles.NameAr'))  }}

                                    </th>
                                    <th>
                                        {{ $t('Roles.NormalizedName') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(roles,index) in resultQuery" v-bind:key="roles.id">
                                    <td>
                                        {{index+1}}
                                    </td>

                                    <td v-if="english=='true'">
                                        <strong v-if="isValid('CanEditUserRole')">
                                            <a href="javascript:void(0)" v-on:click="EditRoles(roles.id)">{{roles.name == 'Sales Man' ? 'Salesman' : roles.name}}</a>
                                        </strong>
                                        <strong v-else>
                                            {{roles.name == 'Sales Man' ? 'Salesman' : roles.name}}
                                        </strong>
                                    </td>
                                    <td v-if="isOtherLang()">
                                        <strong v-if="isValid('CanEditUserRole')">
                                            <a href="javascript:void(0)" v-on:click="EditRoles(roles.id)">{{roles.nameArabic}}</a>
                                        </strong>
                                        <strong v-else>
                                            {{roles.nameArabic}}
                                        </strong>
                                    </td>
                                    <td>
                                        {{roles.normalizedName== 'SALES MAN' ? 'SALESMAN' : roles.normalizedName}}
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <addRoles :newroles="newRoles"
                      :show="show"
                      v-if="show"
                      @close="show = false"
                      :type="type" />
        </div>

    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
           
            return {
                arabic: '',
                english: '',
                searchQuery: '',
                show: false,
                roleslist: [],
                newRoles: {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    normalizedName: '',
                    isActive: true
                },
                type: '',
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.roleslist.filter((city) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => city.name.toLowerCase().includes(v))
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
            openmodel: function () {
                this.newRoles = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    nameArabic: '',
                    description: '',
                    isActive: true
                }
                this.show = !this.show;
                this.type = "Add";
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
            },
            EditRoles: function (Id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/NobleRolesDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newRoles.id = response.data.id;
                            root.newRoles.name = response.data.name;
                            root.newRoles.nameArabic = response.data.nameArabic;
                            root.newRoles.normalizedName = response.data.normalizedName;
                            root.newRoles.isActive = response.data.isActive;
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
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.GetRolesData();
        }
    }
</script>