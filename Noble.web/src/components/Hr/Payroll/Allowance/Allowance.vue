<template>
    <div class="row" v-if="isValid('CanViewAllowance')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('Allowance.Allowance') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Allowance.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('Allowance.Allowance') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddAllowance')" v-on:click="openmodel" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('Allowance.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('Allowance.Close') }}
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <div class="input-group">
                        <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                class="fas fa-search"></i></button>
                        <input v-model="search" type="text" class="form-control" :placeholder="$t('Allowance.Search')"
                            aria-label="Example text with button addon" aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>
                                    <th class="text-center">
                                        {{ $t('Allowance.Code') }}
                                    </th>
                                    <th v-if="english == 'true'">
                                        {{ $t('Allowance.NameEnglish') }}
                                    </th>
                                    <th v-if="isOtherLang()"
                                        v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left' : 'text-right'">
                                        {{ $t('Allowance.NameArabic') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Allowance.AmountPercentage') }}
                                    </th>
                                    <th class="text-center">
                                        {{ $t('Allowance.Taxable') }}
                                    </th>
                                    <th>
                                        {{ $t('Allowance.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(allowance, index) in allowancelist" v-bind:key="allowance.id">
                                    <td v-if="currentPage === 1">
                                        {{ index + 1 }}
                                    </td>
                                    <td v-else>
                                        {{ ((currentPage * 10) - 10) + (index + 1) }}
                                    </td>

                                    <td class="text-center" v-if="isValid('CanEditAllowance')">
                                        <strong>
                                            <a href="javascript:void(0)"
                                                v-on:click="EditAllowance(allowance.id)">{{ allowance.code }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center" v-else>
                                        <strong>
                                            {{ allowance.code }}
                                        </strong>
                                    </td>

                                    <td v-if="english == 'true'">
                                        {{ allowance.allowanceNameEn }}
                                    </td>
                                    <td v-if="isOtherLang()">
                                        {{ allowance.allowanceNameAr }}
                                    </td>
                                    <td class="text-center">
                                        <span v-if="allowance.amountType == 2">{{ currency }} </span> {{ allowance.amount }}
                                        <span v-if="allowance.amountType == 1">%</span>
                                    </td>
                                    <td class="text-center">
                                        {{ allowance.taxPlan == 1 ? $t('Allowance.Taxable') : $t('Allowance.NonTaxable') }}
                                    </td>
                                    <td>
                                        <span v-if="allowance.isActive" class="badge badge-boxed  badge-outline-success">{{ $t('Allowance.Active') }}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{ $t('Allowance.De-Active') }}</span>
                                    </td>
                                   
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-lg-6">
                            <span v-if="currentPage === 1 && rowCount === 0"> {{ $t('Pagination.ShowingEntries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount < 10"> {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === 1 && rowCount >= 11"> {{ $t('Pagination.Showing') }}
                                {{ currentPage }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === 1"> {{ $t('Pagination.Showing') }} {{ currentPage }} {{
                                    $t('Pagination.to')
                            }} {{ currentPage * 10 }} of {{ rowCount }} {{ $t('Pagination.entries')
}}</span>
                            <span v-else-if="currentPage !== 1 && currentPage !== pageCount"> {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ currentPage * 10 }} {{
                                        $t('Pagination.of')
                                }} {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                            <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }}
                                {{ (currentPage * 10) - 9 }} {{ $t('Pagination.to') }} {{ rowCount }} {{ $t('Pagination.of') }}
                                {{ rowCount }} {{ $t('Pagination.entries') }}</span>
                        </div>
                        <div class=" col-lg-6">
                            <div class="float-end" v-on:click="GetAllowanceData()">
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
            </div>

            <addAllowance :newallowance="newAllowance" :show="show" v-if="show" @close="IsSave" :type="type" />
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
            currency: '',
            show: false,
            allowancelist: [],
            newAllowance: {
                id: '',
                allowanceTypeId: '',
                amountType: '',
                taxPlan: '',
                description: '',
                amount: 0,
                code: '',
                isActive: true
            },
            type: '',
            search: '',
            currentPage: 1,
            pageCount: '',
            rowCount: '',
            arabic: '',
            english: '',
        }
    },
    watch: {
        search: function (val) {
            this.GetAllowanceData(val, 1);
        }
    },
    methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },
        IsSave: function () {

            this.show = false;

            this.GetAllowanceData(this.search, this.currentPage);
        },
        getPage: function () {
            this.GetAllowanceData(this.search, this.currentPage);
        },
        openmodel: function () {
            this.newAllowance = {
                id: '00000000-0000-0000-0000-000000000000',
                allowanceTypeId: '',
                amountType: '',
                taxPlan: '',
                description: '',
                amount: 0,
                code: '',
                isActive: true

            }
            this.show = !this.show;
            this.type = "Add";
        },
        GetAllowanceData: function () {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Payroll/AllowanceList?isDropdown=false' + '&pageNumber=' + this.currentPage + '&searchTerm=' + this.search, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.allowancelist = response.data.results;
                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;
                    root.loading = false;
                }
                root.loading = false;
            });
        },
        EditAllowance: function (Id) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/AllowanceDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newAllowance.id = response.data.id;
                        root.newAllowance.allowanceTypeId = response.data.allowanceTypeId;
                        root.newAllowance.amountType = response.data.amountType;
                        root.newAllowance.taxPlan = response.data.taxPlan;
                        root.newAllowance.description = response.data.description;
                        root.newAllowance.code = response.data.code;
                        root.newAllowance.isActive = response.data.isActive;
                        root.newAllowance.amount = response.data.amount;
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
        this.currency = localStorage.getItem('currency');

        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.GetAllowanceData(this.search, 1);

    }
}
</script>