<template>

<div class="row" v-if="isValid('CanViewDeduction')">

<div class="col-lg-12">
    <div class="row">
        <div class="col-sm-12">
            <div class="page-title-box">
                <div class="row">
                    <div class="col">
                        <h4 class="page-title">{{ $t('Deduction.Deduction') }}</h4>
                        <ol class="breadcrumb">
                            <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('Deduction.Home') }}</a></li>
                            <li class="breadcrumb-item active">{{ $t('Deduction.Deduction') }}</li>
                        </ol>
                    </div>
                    <div class="col-auto align-self-center">
                        <a v-if="isValid('CanAddDeduction')" v-on:click="openmodel" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-primary mx-1">
                            <i class="align-self-center icon-xs ti-plus"></i>
                            {{ $t('Deduction.AddNew') }}
                        </a>
                        <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                            class="btn btn-sm btn-outline-danger">
                            {{ $t('Deduction.Close') }}
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
                <input v-model="search" type="text" class="form-control" :placeholder="$t('Deduction.Search')"
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
                                            {{ $t('Deduction.Code') }}
                                        </th>
                                        <th v-if="english=='true'">
                                            {{ $t('Deduction.NameEnglish') }}
                                        </th>
                                        <th v-if="isOtherLang()" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                                            {{ $t('Deduction.NameArabic') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('Deduction.AmountPercentage') }}
                                        </th>
                                        <th class="text-center">
                                            {{ $t('Deduction.Taxable') }}
                                        </th>
                                        <th>
                                            {{ $t('Deduction.Status') }}
                                        </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(deduction ,index) in deductionList" v-bind:key="deduction.id">
                                        <td v-if="currentPage === 1">
                                            {{index+1}}
                                        </td>
                                        <td v-else>
                                            {{((currentPage*10)-10) +(index+1)}}
                                        </td>

                                        <td v-if="isValid('CanEditDeduction')">
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditDeduction(deduction.id)">{{deduction.code}}</a>
                                            </strong>
                                        </td>
                                        <td v-else>
                                            <strong>
                                                {{deduction.code}}
                                            </strong>
                                        </td>
                                        <td v-if="english=='true'">
                                            {{deduction.nameInPayslip}}
                                        </td>
                                        <td v-if="isOtherLang()">
                                            {{deduction.nameInPayslipArabic}}
                                        </td>
                                        <td class="text-center">
                                            {{deduction.amountType==2? currency : ''}} {{parseFloat(deduction.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}} {{deduction.amountType==1? '%' : ''}}
                                        </td>
                                        <td class="text-center">
                                            <span v-if="($i18n.locale == 'en' ||isLeftToRight())"> {{deduction.taxPlan==1? 'Taxable' : 'Non Taxable'}}</span>
                                            <span v-else> {{deduction.taxPlan==1? 'خاضع للضريبة' : 'غير خاضعة للضريبة'}}</span>                                           
                                        </td>
                                        <td>
                                            <span v-if="deduction.active" class="badge badge-boxed  badge-outline-success">{{ $t('Deduction.Active') }}</span>
                                            <span v-else class="badge badge-boxed  badge-outline-danger">{{ $t('Deduction.De-Active') }}</span>
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
                    <div class="float-end" v-on:click="GetBrandData()">
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

    <add-deduction :newDeduction="newDeduction"
                       :show="show"
                       v-if="show"
                       @close="IsSave"
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
        data: function () {
            return {
                currency: '',
                searchQuery: '',
                show: false,
                deductionList: [],
                newDeduction: {
                    id: '',
                    nameInPayslip: '',
                    nameInPayslipArabic: '',
                    amountType: '',
                    taxPlan: '',
                    code: '',
                    amount: 0,
                    active: true
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
            search: function () {
                this.GetDeductionData();
            }
        },
        methods: {
            GotoPage: function (link) {
                this.$router.push({path: link});
            },
            IsSave: function () {
                this.show = false;
                this.GetDeductionData();
            },

            getPage: function () {
                this.GetDeductionData();
            },

            openmodel: function () {
                this.newDeduction = {
                    id: '00000000-0000-0000-0000-000000000000',
                    nameInPayslip: '',
                    nameInPayslipArabic: '',
                    amountType: '',
                    taxPlan: '',
                    code: '',
                    amount: 0,
                    active: true
                }
                this.show = !this.show;
                this.type = "Add";
            },

            GetDeductionData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('Payroll/DeductionList?searchTerm=' + this.search + '&pageNumber=' + this.currentPage, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.deductionList = response.data.results;
                        root.pageCount = response.data.pageCount;
                        root.rowCount = response.data.rowCount;
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

            EditDeduction: function (Id) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/Payroll/DeductionDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data) {
                            root.newDeduction.id = response.data.id;
                            root.newDeduction.nameInPayslip = response.data.nameInPayslip;
                            root.newDeduction.nameInPayslipArabic = response.data.nameInPayslipArabic;
                            root.newDeduction.amount = response.data.amount;
                            root.newDeduction.code = response.data.code;
                            root.newDeduction.active = response.data.active;
                            
                            if ((root.$i18n.locale == 'en' || root.isLeftToRight())) {
                                root.newDeduction.amountType = response.data.amountType == 1 ? '% of Salary' : 'Fixed';
                                root.newDeduction.taxPlan = response.data.taxPlan == 1 ? 'Taxable' : 'Non Taxable';
                            }
                            else {
                                root.newDeduction.amountType = response.data.amountType == 1 ? '٪ من الراتب' : 'مثبت';
                                root.newDeduction.taxPlan = response.data.taxPlan == 1 ? 'خاضع للضريبة' : 'غير خاضعة للضريبة';
                            }
                            root.show = !root.show;
                            root.type = "Edit"
                        } else {
                            console.log("error: something wrong from db.");
                        }
                    },
                        function (error) {
                            this.loading = false;
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
            this.currency = localStorage.getItem('currency');
            this.GetDeductionData();

        }
    }
</script>