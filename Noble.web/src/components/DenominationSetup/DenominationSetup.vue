<template>
    <div class="row" v-if="isValid('CanViewDenomination')">
        

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('DenominationSetup.DenominationSetup') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('DenominationSetup.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('DenominationSetup.DenominationSetup') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddDenomination')" v-on:click="AddDenominationSetup"
                                   href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('DenominationSetup.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('DenominationSetup.Close') }}
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
                        <input v-model="searchQuery" type="text" class="form-control" :placeholder="$t('DenominationSetup.Search')"
                               aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>
                                        #
                                    </th>
                                    <th class="text-center">
                                        {{ $t('DenominationSetup.DenominationNumber') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('DenominationSetup.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details, index) in resultQuery" v-bind:key="details.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <td v-if="isValid('CanEditDenomination')" class="text-center">
                                        <strong>
                                            <a href="javascript:void(0)"
                                               v-on:click="EditDenominationSetup(details.id)">{{ details.number }}</a>
                                        </strong>
                                    </td>
                                    <td v-else class="text-center">{{ details.number }}</td>
                                    <td class="text-center">
                                        <span v-if="details.isActive"
                                              class="badge badge-boxed  badge-outline-success">
                                            {{
                                                    $t('DenominationSetup.Active')
                                            }}
                                        </span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">
                                            {{
                                                $t('DenominationSetup.De-Active')
                                            }}
                                        </span>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <denominationSetupmodel :newDenominationSetup="newDenominationSetup"
                                    :show="show"
                                    v-if="show"
                                    @close="show = false"
                                    :type="type" />
        </div>

    </div>
    <div v-else>
        <acessdenied></acessdenied>
    </div>


   

</template>
<script>
import clickMixin from '@/Mixins/clickMixin'

export default {
    mixins: [clickMixin],
    name: 'DenominationSetup',
    data: function () {
        return {
            arabic: '',
            english: '',
            show: false,
            searchQuery: '',
            denominationSetuplist: [

            ],
            newDenominationSetup: {
                id: '00000000-0000-0000-0000-000000000000',
                number: '',
                isActive: true
            },
            type: '',

        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {

                return this.denominationSetuplist.filter((cur) => {
                    return root.searchQuery.toLowerCase().split(' ').every(v => cur.number.toString().includes(v))
                })
            } else {
                return root.denominationSetuplist;
            }
        },
    },
    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },

        AddDenominationSetup: function () {
            this.newDenominationSetup = {
                id: '00000000-0000-0000-0000-000000000000',
                number: '',
                isActive: true
            };
            this.show = !this.show;
            this.type = "Add";
        },
        GetDenominationSetupData: function () {
            var root = this;
            var url = '/Product/DenominationSetupList';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {

                    root.denominationSetuplist = response.data.denominationSetups;
                }
            });
        },
        EditDenominationSetup: function (id) {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/DenominationSetupDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newDenominationSetup.id = response.data.id;
                        root.newDenominationSetup.number = response.data.number;
                        root.newDenominationSetup.isActive = response.data.isActive;
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
        this.GetDenominationSetupData();
    }
}
</script>