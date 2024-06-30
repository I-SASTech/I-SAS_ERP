<template>
    <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card ">
                <div class="BorderBottom ml-4 mr-4 mt-3 mb-3">
                    <span class=" DayHeading">{{ $t('City.city') }}</span>
                </div>


                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="row" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label>{{ $t('City.SearchByCurrency') }}</label>
                                    <div>
                                        <input type="text" class="form-control search_input" v-model="searchQuery" name="search" id="search" :placeholder="$t('City.Search')" />
                                        <span class="fas fa-search search_icon"></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" style="margin-top:18px;">
                                <a href="javascript:void(0)" class="btn btn-primary" v-on:click="openmodel"><i class="fa fa-plus"></i>  {{ $t('City.AddNew') }}</a>
                                <router-link :to="'/GeographicalSetup'">
                                    <a href="javascript:void(0)" class="btn btn-outline-danger ">  <i class="fas fa-arrow-circle-left fa-lg"></i> </a>
                                </router-link>
                            </div>

                        </div>

                        <div class="mt-2">
                            <div>
                                <table class="table table-striped table-hover table_list_bg" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                    <thead class="">
                                        <tr>
                                            <th>#</th>
                                            <th>
                                                {{ $t('City.Code') }}
                                            </th>
                                            <th>
                                                {{ $t('City.CityName') }}
                                            </th>
                                            <th width="30%" class="text-center">
                                                {{ $t('City.Description') }}
                                            </th>
                                            <th>
                                                {{ $t('City.Status') }}
                                            </th>


                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr v-for="(city,index) in resultQuery" v-bind:key="city.id">
                                            <td>
                                                {{index+1}}
                                            </td>

                                            <td>
                                                <strong>
                                                    <a href="javascript:void(0)" v-on:click="EditCity(city.id)">  {{city.code}}</a>
                                                </strong>
                                            </td>

                                            <td>
                                                {{city.name}}
                                            </td>
                                            <td class="text-center">
                                                {{city.description}}
                                            </td>
                                            <td>{{city.isActive==true?$t('City.Active'):$t('City.De-Active')}}</td>



                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <citymodal :newcity="newCity"
                   :show="show"
                   v-if="show"
                   @close="show = false"
                   :type="type" />

    </div>


</template>

<script>
    export default {
        data: function () {
            return {
                searchQuery: '',
                show: false,
                citylist: [],
                newCity: {
                    id: '',
                    name: '',
                    description: '',
                    code: '',
                    isActive: true
                },
                type: '',
            }
        },
        computed: {
            resultQuery: function () {
                var root = this;
                if (this.searchQuery) {
                    return this.citylist.filter((city) => {
                        return root.searchQuery.toLowerCase().split(' ').every(v => city.name.toLowerCase().includes(v) || city.code.toLowerCase().includes(v))
                    })
                } else {
                    return root.citylist;
                }
            },
        },
        methods: {
            openmodel: function () {
                this.newCity = {
                    id: '00000000-0000-0000-0000-000000000000',
                    name: '',
                    description: '',
                    isActive: true

                }
                this.show = !this.show;
                this.type = "Add";
            },
            GetCityData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('Region/CityList?isActive=false', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.citylist = response.data.citys;
                    }
                });
            },
            EditCity: function (Id) {


                var root = this;

                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Region/CityDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {

                            root.newCity.id = response.data.id;
                            root.newCity.name = response.data.name;
                            root.newCity.description = response.data.description;
                            root.newCity.code = response.data.code;
                            root.newCity.isActive = response.data.isActive;
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
            this.GetCityData();
        }
    }
</script>