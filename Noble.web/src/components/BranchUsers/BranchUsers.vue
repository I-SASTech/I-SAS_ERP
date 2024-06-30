<template>
    <div class="row" >

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('BranchUsers.BranchUsers') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('BranchUsers.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('BranchUsers.BranchUsers') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">

                                <a  v-on:click="openmodel"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('BranchUsers.AddBranch') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('BranchUsers.Close') }}
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
                                        {{ $t('BranchUsers.UserName') }}
                                    </th>
                                    <th>
                                        {{ $t('BranchUsers.BranchName') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(terminal,index) in resultQuery" v-bind:key="index">
                                    <td>
                                        {{index+1}}
                                    </td>

                                    <td v-if="isValid('CanEditTerminal') || isValid('Noble Admin')">
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditTerminal(terminal.userId)">{{terminal.userName}}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{terminal.userName}}
                                    </td>
                                    <td>
                                        <span v-for="(item, index) in terminal.branchId" v-bind:key="item.id">
                                            {{index != 0?', '+item.name : item.name}}
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />


                </div>
            </div>

            <addbranchuser :terminal="newBranch"
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
                    userId: '',
                    locationId: '',
                    branchId: [],
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
                    userId: '',
                    locationId: '',
                    branchId: [],
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

                root.$https.get('Branches/BranchUserList?isDropdown=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
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
                root.$https.get('/Branches/GetBranchUserDetail?id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newBranch.id = response.data.id;
                            root.newBranch.userId = response.data.userId;
                            root.newBranch.locationId = response.data.locationId;
                            root.newBranch.branchId = response.data.branchId;

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