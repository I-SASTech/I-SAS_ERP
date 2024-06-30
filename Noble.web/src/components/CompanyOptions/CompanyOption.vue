<template>
    <div class="row" v-if="isValid('Can View Color')">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card">
                <div class="BorderBottom ml-2 mr-2 mt-3 mb-3">
                    <span class=" DayHeading">{{ $t('CompanyOption.CompanyOption') }}</span>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                            <div class="form-group">
                                <label>{{ $t('CompanyOption.SearchbyLabel') }}</label>
                                <div>
                                    <input type="text" class="form-control search_input" v-model="search" name="search" id="search" :placeholder="$t('CompanyOption.Search')" />
                                    <span class="fas fa-search search_icon"></span>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                            <a href="javascript:void(0)" class="btn btn-primary" style="margin-top:27px;" v-on:click="openmodel"><i class="fa fa-plus"></i>  {{ $t('CompanyOption.AddNew') }}</a>
                            <router-link :to="'/ProductManagement'">
                                <a href="javascript:void(0)" class="btn btn-outline-danger " style="margin-top:27px;">  <i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                            </router-link>
                        </div>

                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                            <table class="table table-striped table-hover table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <thead class="">
                                    <tr>
                                        <th>#</th>
                                        <th v-if="english=='true'" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('CompanyOption.Label')}}
                                        </th>
                                        <th v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('CompanyOption.Status') }}
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(option,index) in companyOptionList" v-bind:key="option.id">
                                        <td>
                                            {{index+1}}
                                        </td>

                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditColor(option.id)">  {{option.label}}</a>
                                            </strong>
                                        </td>
                                        <td v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">{{option.value==true?$t('CompanyOption.Active'):$t('CompanyOption.De-Active')}}</td>
                                    </tr>
                                </tbody>
                            </table>

                        </div>
                    </div>
                </div>
            </div>
             <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>
        <add-company-option :show="show"
                            :companyOptions="companyOption"
                            v-if="show"
                            @close="IsSave"
                            :type="type" />

    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    export default {
        mixins: [clickMixin],
         components: {
            Loading
        },
        data: function () {
            return {
                  loading: false,
                arabic: '',
                english: '',
                searchQuery: '',
                show: false,
                companyOptionList: [],
                type: '',
                search: '',
                companyOption: {
                    id: '00000000-0000-0000-0000-000000000000',
                    label: '',
                    value: true
                },
            }
        },
        watch: {
            search: function (val) {
                this.GetColorData(val);
            }
        },
        methods: {
            IsSave: function () {

                this.show = false;

                this.GetColorData(this.search);
            },
            getPage: function () {
                this.GetColorData(this.search);
            },
            openmodel: function () {
                this.show = !this.show;
                this.type = "Add";
            },
            GetColorData: function (search) {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Company/CompanyOptionList?searchTerm=' + search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        root.companyOptionList = response.data;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            EditColor: function (Id) {
                this.loading = true;
                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Company/CompanyOptionDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.companyOption.id = response.data.id;
                            root.companyOption.label = response.data.label;
                            root.companyOption.value = response.data.value;
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
            this.GetColorData(this.search);
        }
    }
</script>