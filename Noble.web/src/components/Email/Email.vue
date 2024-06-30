<template>
    <div class="row" v-if="isValid('BasicMail') || isValid('StandardMail')">      
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Email Configuration List') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('color.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Email Configuration List') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddColor')" v-on:click="openmodel()" href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('color.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);" class="btn btn-sm btn-outline-danger">
                                    {{ $t('color.Close') }}
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
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i class="fas fa-search"></i></button>
                                <input v-model="search" type="text" class="form-control" :placeholder="$t('color.Search')" aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1">

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
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th >
                                        {{($t('Email'))   }}
                                    </th>

                                    <th>
                                        {{ $t('Password') }}
                                    </th>
                                    <th >
                                        {{ $t('SMTP Server') }}
                                    </th>
                                    <th >
                                        {{ $t('Port') }}
                                    </th>
                                    <th >
                                        {{ $t('Default') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                               <tr v-for="(item, index) in emailConfigurationList" :key="item.id">
                                    <td v-if="currentPage === 1">
                                        {{index+1}}
                                    </td>
                                    <td v-else>
                                        {{((currentPage*10)-10) +(index+1)}}
                                    </td>
                                    <td>
                                        <strong>
                                            <a href="javascript:void(0)" v-on:click="EditColor(item.id)">{{item.email}}</a>
                                        </strong>
                                    </td>
                                    <td>
                                        {{ item.password }}
                                    </td>
                                    <td>
                                        {{ item.smtpServer }}
                                    </td>
                                    <td>
                                        {{ item.port }}
                                    </td>
                                    <td>
                                        <input type="checkbox" :id="index + 1" v-model="item.isDefault"  v-on:change="makeDefaultEmail(item.id)" />
                                    </td>
                               </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                    <div class="float-start">
                        <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount < 10">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1 && rowCount >= 11  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                        <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                    </div>
                    <div class="float-end">
                        <div class="" v-on:click="GetColorData()">
                            <b-pagination pills size="sm" v-model="currentPage"
                                              :total-rows="rowCount"
                                              :per-page="10"
                                              :first-text="$t('Table.First')"
                                              :prev-text="$t('Table.Previous')"
                                              :next-text="$t('Table.Next')"
                                              :last-text="$t('Table.Last')" ></b-pagination>
                        </div>
                    </div>
                </div>
            </div>
            <EmailModal :newEmailConfiguration="newEmailConfiguration"
                        :show="show"
                        v-if="show"
                        @close="IsSave"
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
                emailConfigurationList: [],
                newEmailConfiguration: {
                    id: '',
                    email: '',
                    password: '',
                    smtpServer: '',
                    port: '',
                    server:'',
                    isActive: false,
                },
                type: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
            }
        },
        methods: {
            makeDefaultEmail(id)
            {
                var root = this;
                var token = '';

                if (token == '') 
                {
                    token = localStorage.getItem('token');
                }

                root.$https.get('Product/UpdateDefaultEmailConfiguration?id=' + id , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess) {
                        root.$swal({
                            title: root.$t('AddColors.SavedSuccessfully'),
                            text: root.$t('AddColors.Saved'),
                            type: 'success',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.GetColorData(root.search, root.currentPage);
                    }
                    else
                    {
                        root.$swal({
                            title: 'Error',
                            text: response.data.isAddUpdate,
                            type: 'error',
                            icon: 'error',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.GetColorData(root.search, root.currentPage);
                    }
                }).catch(error => {
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: this.$t('AddColors.SomethingWrong'),
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    });
            },
            search22: function () {
                this.GetColorData(this.search, this.currentPage, this.active);
            },

            clearData: function () {
                this.search = "";
                this.GetColorData(this.search, this.currentPage, this.active);
            },

            GotoPage: function (link) {
                this.$router.push({ path: link });
            },
            IsSave: function () {

                this.show = false;

                this.GetColorData(this.search, this.currentPage);
            },
            getPage: function () {
                this.GetColorData(this.search, this.currentPage);
            },
            openmodel: function () {
                this.newEmailConfiguration = {
                    id: '00000000-0000-0000-0000-000000000000',
                    email: '',
                    password: '',
                    smtpServer: '',
                    port: '',
                    server:'',
                    isActive: true,
                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetColorData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Product/EmailConfigurationList?isDropdown=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.emailConfigurationList = response.data.results.emailConfigurations;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditColor: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Product/GetEmailConfigurationDetails?id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newEmailConfiguration.id = response.data.id;
                            root.newEmailConfiguration.email = response.data.email;
                            root.newEmailConfiguration.password = response.data.password;
                            root.newEmailConfiguration.smtpServer = response.data.smtpServer;
                            root.newEmailConfiguration.port = response.data.port;
                            root.newEmailConfiguration.server = response.data.server;
                            root.newEmailConfiguration.isActive = response.data.isActive;
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
            this.GetColorData(this.search, 1);
        }
    }
</script>