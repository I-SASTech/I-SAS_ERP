<template>
    <div class="row" v-if="isValid('CanViewVatRate')">        

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('TaxRate.AddTaxRate') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('TaxRate.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('TaxRate.AddTaxRate') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddVatRate')" v-on:click="openmodel" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('TaxRate.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                   class="btn btn-sm btn-outline-danger">
                                    {{ $t('TaxRate.Close') }}
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
                        <input v-model="searchQuery" type="text" class="form-control"
                               :placeholder="$t('TaxRate.Search')" aria-label="Example text with button addon"
                               aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th>
                                        {{ $t('TaxRate.Code') }}
                                    </th>
                                    <th>
                                        {{$englishLanguage($t('TaxRate.TaxRateNameEn'))   }}
                                    </th>
                                    <th>
                                        {{$arabicLanguage($t('TaxRate.TaxRateNameAr'))   }}
                                    </th>

                                    <th>
                                        {{ $t('TaxRate.Description') }}
                                    </th>
                                    <th>
                                        {{ $t('TaxRate.TaxRate') }}
                                    </th>
                                    <th>
                                        {{ $t('TaxRate.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(taxRate, index) in resultQuery" v-bind:key="taxRate.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>

                                    <td v-if="isValid('CanEditVatRate')">
                                        <span v-if="taxRate.id == taxRateId"
                                              style="color:red !important; font-weight:bold">
                                            <a>{{ taxRate.code }}</a>
                                        </span>
                                        <strong v-else>
                                            <a href="javascript:void(0)"
                                               v-on:click="EditTaxRate(taxRate.id)">{{ taxRate.code }}</a>
                                        </strong>
                                    </td>
                                    <td v-else>
                                        {{ taxRate.code }}
                                    </td>
                                    <td v-if="taxRate.id == taxRateId">
                                        <span style="font-weight:bold">{{ $t('TaxRate.Default') }}(</span>
                                        {{ taxRate.name }} <span style="font-weight:bold">)</span>
                                    </td>
                                    <td v-else>
                                        {{ taxRate.name }}
                                    </td>
                                    <td v-if="taxRate.id == taxRateId">
                                        <span style="font-weight:bold">{{ $t('TaxRate.Default') }}(</span>
                                        {{ taxRate.nameArabic }} <span style="font-weight:bold">)</span>
                                    </td>
                                    <td v-else>
                                        {{ taxRate.nameArabic }}
                                    </td>

                                    <td>
                                        {{ taxRate.description }}
                                    </td>
                                    <td v-if="index == 0">
                                        {{ taxRate.rate }} <span style="font-weight:bold">({{ taxMethod }})</span>
                                    </td>
                                    <td v-else>
                                        {{ taxRate.rate }}
                                    </td>
                                    <td>
                                        <span v-if="taxRate.isActive"
                                              class="badge badge-boxed  badge-outline-success">{{ $t('TaxRate.Active') }}</span>
                                        <span v-else
                                              class="badge badge-boxed  badge-outline-danger">{{ $t('TaxRate.De-Active') }}</span>
                                    </td>
                                </tr>



                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <taxratemodel :newtaxrate="newTaxRate" :show="show" v-if="show" @close="show = false" :type="type" />
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
    data: function () {
        return {
            searchQuery: '',
            show: false,
            taxRatelist: [],
            newTaxRate: {
                id: '',
                name: '',
                taxMethod: '',
                nameArabic: '',
                description: '',
                code: '',
                rate: 0,
                isActive: true
            },
            type: '',
            taxRateId: '',
        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return this.taxRatelist.filter((taxRate) => {
                    return root.searchQuery.toLowerCase().split(' ').every(v => taxRate.name.toLowerCase().includes(v) || taxRate.nameArabic.toLowerCase().includes(v))
                })
            } else {
                return root.taxRatelist;
            }
        },
    },
    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },

        openmodel: function () {
            this.newTaxRate = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                description: '',
                code: '',
                rate: 0,
                isActive: true
            }
            this.show = !this.show;
            this.type = "Add";
        },
        GetTaxRateData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Product/TaxRateList?isActive=false', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.taxRatelist = response.data.taxRates;
                    root.taxMethod = response.data.taxMethod;
                }
            });
        },
        EditTaxRate: function (Id) {
            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/TaxRateDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {

                        root.newTaxRate.id = response.data.id;
                        root.newTaxRate.name = response.data.name;
                        root.newTaxRate.nameArabic = response.data.nameArabic;
                        root.newTaxRate.rate = response.data.rate;
                        root.newTaxRate.description = response.data.description;
                        root.newTaxRate.code = response.data.code;
                        root.newTaxRate.isActive = response.data.isActive;
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

        this.taxRateId = localStorage.getItem('TaxRateId')
    },
    mounted: function () {
        this.GetTaxRateData();
    }
}
</script>