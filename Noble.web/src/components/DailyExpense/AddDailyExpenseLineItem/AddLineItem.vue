<template>
    <div >
        <div v-if="isDisable=='true'">
            <div class=" mt-4 " v-bind:class="{ 'horizontal-table': tableLength >= cardLength }">
                <table class="table mb-0">
                    <thead class="thead-light table-hover">
                        <tr class="text-capitalize text-center">
                            <th style="width: 5%;"></th>
                            <th style="width:50%;" v-if="!IsExpenseAccount">{{ $t('AddLineItem.ExpenseDescription') }}</th>
                            <th style="width:20%;" v-else>{{ $t('AddLineItem.ExpenseDescription') }}</th>
                            <th style="width:25%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.ExpenseAccount') }}</th>
                            <th style="width:30%;" class="text-center" v-if="!IsExpenseAccount">{{ $t('AddLineItem.Amount') }}</th>
                            <th style="width:5%;" class="text-center" v-if="IsExpenseAccount">{{ $t('AddLineItem.Amount') }}</th>
                            <th style="width:15%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.TaxMethod') }}</th>
                            <th style="width:15%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.VAT%') }}</th>
                            <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">{{ $t('AddLineItem.TotalVAT') }}</th>
                            <th style="width:10%;" class="text-center" v-if="IsExpenseAccount">{{ $t('AddLineItem.Total') }}</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(expense,index) in dailyExpenseRow" v-bind:key="expense.id">
                            <td>{{index+1}}</td>
                            <td>
                                <input class="form-control input-border" v-model="expense.description" v-bind:placeholder="$t('AddLineItem.WriteHere')" />
                            </td>
                            <td v-if="IsExpenseAccount">
                                <accountdropdown v-model="expense.expenseAccountId" :disabled="true" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'" />
                            </td>
                            <td class="text-center">
                                {{expense.amount}}
                            </td>
                            <td v-if="IsExpenseAccount">
                                <multiselect :options="options1" disabled v-model="expense.taxMethod" @update:modelValue="updateLineTotal(expense.taxMethod, 'taxMehtod', expense)" :show-labels="false" v-bind:placeholder="$t('AddLineItem.SelectMethod')">
                                </multiselect>
                            </td>
                            <td v-if="IsExpenseAccount && (expense.taxMethod=='Exempted' || expense.taxMethod=='معفى') ">
                                <taxratedropdown :isDisable="'true'" v-model="expense.vatId" :modelValue="expense.vatId" :key="randerTaxRate" @update:modelValue="updateLineTotal(expense.vatId, 'vat',expense)" />
                            </td>
                            <td v-else-if="IsExpenseAccount">
                                <taxratedropdown v-model="expense.vatId" :isDisable="'true'" :modelValue="expense.vatId" @update:modelValue="updateLineTotal(expense.vatId, 'vat',expense)" />
                            </td>

                            <td v-if="IsExpenseAccount" class="text-center">
                                {{parseFloat(expense.vatAmount==null ||expense.vatAmount=='' || expense.vatAmount==undefined?0:expense.vatAmount ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td v-if="IsExpenseAccount" class="text-center">
                                {{parseFloat(expense.amountAfterVAT==null ||expense.amountAfterVAT=='' || expense.amountAfterVAT==undefined?0:expense.amountAfterVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                        </tr>
                    </tbody>

                </table>

            </div>
            <div class="row">
                <div class=" col-lg-6 "></div>
                <div class=" col-lg-6 ">
                    <div class="mt-4">
                        <table class="table" style="background-color: #f1f5fa;">
                            <tbody>
                                <tr>
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold">{{ $t('AddLineItem.TotalItem') }}</span>
                                    </td>
                                    <td class="text-end" style="width:35%;">  {{ summary.item }}</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold"> {{ $t('AddLineItem.Amount') }} </span>
                                    </td>
                                    <td class="text-end" style="width:35%;">{{currency}}  {{ summary.total }}</td>
                                </tr>
                                <tr v-if="IsExpenseAccount">
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold"> {{ $t('AddLineItem.TotalVAT') }} </span>
                                    </td>
                                    <td class="text-end" style="width:35%;">    {{currency}} {{parseFloat(summary.vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr v-if="IsExpenseAccount">
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold" v-if="IsExpenseAccount"> {{ $t('AddLineItem.Total') }} </span>
                                    </td>
                                    <td class="text-end" style="width:35%;">  {{currency}}   {{parseFloat(summary.amountAfterVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


                </div>

            </div>

        </div>
        <div  v-else>
            <div class="  mt-4 " v-bind:class="{ 'horizontal-table': tableLength >= cardLength }">
                <table class="table">
                    <thead class="thead-light table-hover">
                        <tr class="text-capitalize text-center">
                            <th class="text-center" v-if="!IsExpenseAccount" style="width: 5%;" >#</th>
                            <th class="text-center" v-else style="width: 3%;" >#</th>
                            <th style="width:60%;" v-if="!IsExpenseAccount">{{ $t('AddLineItem.ExpenseDescription') }}</th>
                            <th style="width:30%;" v-if="!IsExpenseAccount" class="text-center" >{{ $t('AddLineItem.Amount') }}</th>
                            <th style="width:16%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.ExpenseDescription') }}</th>
                            <th style="width:25%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.ExpenseAccount') }}</th>
                            <th style="width:8%;" class="text-center" v-if="IsExpenseAccount">{{ $t('AddLineItem.Amount') }}</th>
                            <th style="width:14%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.TaxMethod') }}</th>
                            <th style="width:15%;" v-if="IsExpenseAccount">{{ $t('AddLineItem.VAT%') }}</th>
                            <th style="width:8%;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" v-if="IsExpenseAccount">{{ $t('AddLineItem.TotalVAT') }}</th>
                            <th style="width:8%;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'" v-if="IsExpenseAccount">{{ $t('AddLineItem.Total') }}</th>
                            <th style="width:3%;" class="text-center"></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(expense,index) in dailyExpenseRow" v-bind:key="expense.id">
                            <td class="text-center">{{index+1}}</td>
                            <td>
                                <input class="form-control" v-model="expense.description" style="background-color: #ffffff; height: 40px;" v-bind:placeholder="$t('AddLineItem.WriteHere')" />
                            </td>
                            <td v-if="IsExpenseAccount">
                                <accountdropdown v-model="expense.expenseAccountId" :PanelWidth="true" :disabled="isExpensesDisable" :dropdownaccount="'dropdownAccountcss'" :dropdownpo="'dropdownpo'"  :formNames="'Expenses'" :isExpenses="isExpenses"/>
                            </td>
                            <td class="text-center">
                                <decimal-to-fixed style="height:40px;" v-model="expense.amount"  @update:modelValue="updateLineTotal(expense.amount, 'amount', expense)" />
                            </td>
                            <td v-if="IsExpenseAccount">
                                <multiselect :options="options1" v-model="expense.taxMethod" @update:modelValue="updateLineTotal(expense.taxMethod, 'taxMehtod', expense)" :show-labels="false" v-bind:placeholder="$t('AddLineItem.SelectMethod')">
                                </multiselect>
                            </td>
                            <td v-if="IsExpenseAccount && (expense.taxMethod=='Exempted' || expense.taxMethod=='معفى') ">
                                <taxratedropdown :isDisable="'true'" v-model="expense.vatId" :modelValue="expense.vatId" :key="randerTaxRate" @update:modelValue="updateLineTotal(expense.vatId, 'vat',expense)" />

                            </td>
                            <td v-else-if="IsExpenseAccount">
                                <taxratedropdown v-model="expense.vatId" :PanelWidth="true" :modelValue="expense.vatId" @update:modelValue="updateLineTotal(expense.vatId, 'vat',expense)" />

                            </td>

                            <td v-if="IsExpenseAccount" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                {{parseFloat(expense.vatAmount==null ||expense.vatAmount=='' || expense.vatAmount==undefined?0:expense.vatAmount ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>
                            <td v-if="IsExpenseAccount" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                {{parseFloat(expense.amountAfterVAT==null ||expense.amountAfterVAT=='' || expense.amountAfterVAT==undefined?0:expense.amountAfterVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                            </td>

                            <td class="text-center" v-if="index == dailyExpenseRow.length - 1 && addItem == false && dailyExpenseRow[index].description!='' && dailyExpenseRow[index].amount!=0">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right  " v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="index == dailyExpenseRow.length - 1 && addItem == false && dailyExpenseRow[index].description=='' && dailyExpenseRow[index].amount==0">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right  " disabled v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="index == dailyExpenseRow.length - 1 && addItem == false && dailyExpenseRow[index].description==''">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right  " disabled v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="index == dailyExpenseRow.length - 1 && addItem == false && dailyExpenseRow[index].amount==0">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right  " disabled v-on:click="addDailyExpense">
                                    <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else>
                                <button title="Remove Item" class="btn   btn-sm   " v-on:click="removeExpense(expense.id, 'true')">
                                    <i data-v-59af708c="" class="las la-trash-alt text-danger font-16"></i>
                                </button>
                            </td>
                        </tr>

                        <tr v-if="addItem">
                            <td class="text-center">{{dailyExpenseRow.length+1}}</td>
                            <td>
                                <input class="form-control input-border" v-model="dailyExpenses.description" v-bind:placeholder="$t('AddLineItem.WriteHere')" style="width:100% !important;height:40px;" />
                            </td>
                            <td v-if="IsExpenseAccount">
                                <accountdropdown v-model="dailyExpenses.expenseAccountId" :PanelWidth="true" :disabled="isExpensesDisable" :dropdownaccount="'dropdownAccountcss'" :value="dailyExpenses.expenseAccountId" :dropdownpo="'dropdownpo'" :key="refresh" :formNames="'Expenses'" :isExpenses="isExpenses" />

                            </td>
                            <td>
                                <decimal-to-fixed v-model="dailyExpenses.amount" :key="refresh"  @update:modelValue="updateLineTotal(dailyExpenses.amount, 'amount', dailyExpenses)" />
                            </td>
                            <td v-if="IsExpenseAccount">
                                <multiselect :options="options1" v-model="dailyExpenses.taxMethod" :show-labels="false" v-bind:placeholder="$t('AddLineItem.SelectMethod')" @update:modelValue="updateLineTotal(dailyExpenses.taxMethod, 'taxMehtod', dailyExpenses)">
                                </multiselect>
                            </td>
                            <td v-if="IsExpenseAccount && (dailyExpenses.taxMethod=='Exempted' || dailyExpenses.taxMethod=='معفى') ">
                                <taxratedropdown :isDisable="'true'" :key="randerTaxRate" @update:modelValue="updateLineTotal(dailyExpenses.vatId, 'vat',dailyExpenses)" />
                            </td>
                            <td v-else-if="IsExpenseAccount ">
                                <taxratedropdown v-model="dailyExpenses.vatId" :PanelWidth="true" :modelValue="dailyExpenses.vatId" :key="refresh" @update:modelValue="updateLineTotal(dailyExpenses.vatId, 'vat',dailyExpenses)" />
                            </td>


                            <td v-if="IsExpenseAccount" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                {{parseFloat(dailyExpenses.vatAmount==null || dailyExpenses.vatAmount=='' || dailyExpenses.vatAmount==undefined?0:dailyExpenses.vatAmount ).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                            </td>
                            <td v-if="IsExpenseAccount" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                {{parseFloat(dailyExpenses.amountAfterVAT==null ||dailyExpenses.amountAfterVAT=='' || dailyExpenses.amountAfterVAT==undefined?0:dailyExpenses.amountAfterVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}

                            </td>

                            <td class="text-center" v-if="(dailyExpenses.description=='' ) ">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " disabled v-on:click="addDailyExpense">
                                   <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="( dailyExpenses.amount==0) ">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " disabled v-on:click="addDailyExpense">
                                   <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else-if="IsExpenseAccount">
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " v-if="dailyExpenses.expenseAccountId=='' || dailyExpenses.taxMethod == '' || (dailyExpenses.vatId == '' && dailyExpenses.vatAmount == 0 )" disabled v-on:click="addDailyExpense">
                                   <i class="fa fa-check"></i>
                                </button>
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " v-else v-on:click="addDailyExpense">
                                   <i class="fa fa-check"></i>
                                </button>
                            </td>
                            <td class="text-center" v-else>
                                <button title="Add New Item" class="btn btn-primary btn-sm btn-round btn-icon float-right " v-on:click="addDailyExpense">
                                   <i class="fa fa-check"></i>
                                </button>
                            </td>
                        </tr>
                    </tbody>
                </table>

            </div>
            <div class="row">
                <div class=" col-lg-6 "></div>
                <div class=" col-lg-6 ">
                    <div class="mt-4">
                        <table class="table" style="background-color: #f1f5fa;">
                            <tbody>
                                <tr>
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold">{{ $t('AddLineItem.TotalItem') }}</span>
                                    </td>
                                    <td class="text-end" style="width:35%;">  {{ summary.item }}</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold"> {{ $t('AddLineItem.Amount') }} </span>
                                    </td>
                                    <td class="text-end" style="width:35%;">{{currency}}  {{ summary.total }}</td>
                                </tr>
                                <tr v-if="IsExpenseAccount">
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold"> {{ $t('AddLineItem.TotalVAT') }} </span>
                                    </td>
                                    <td class="text-end" style="width:35%;">    {{currency}} {{parseFloat(summary.vatAmount).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                                <tr v-if="IsExpenseAccount">
                                    <td colspan="2" style="width:65%;">
                                        <span class="fw-bold" v-if="IsExpenseAccount"> {{ $t('AddLineItem.Total') }} </span>
                                    </td>
                                    <td class="text-end" style="width:35%;">  {{currency}}   {{parseFloat(summary.amountAfterVAT).toFixed(3).slice(0,-1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}</td>
                                </tr>
                            </tbody>
                        </table>
                    </div>


                </div>

            </div>

 <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

        </div>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
   import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import Multiselect from 'vue-multiselect'
    export default {
        mixins: [clickMixin],
        props: ['BillerRecord', 'isDisable','formName'],
        components: {
            Multiselect,
            Loading
        },
        data: function () {
            return {
                dailyExpenseRow: [],
                selectedValue: [],
                selectedValue1: [],
                options: [],
                options1: [],
                dailyExpenses: {
                    id: '',
                    taxMethod: '',
                    vatId: '',
                    description: '',
                    amount: 0.00,
                    amountAfterVAT: 0.00,
                    vatAmount: 0.00,
                    expenseAccountId: '',
                    dailyExpenseRow: []
                },
                loading: false,
                refresh: 0,
                randerTaxRate: 0,
                currentItem: {
                    id: '',
                    taxMethod: '',
                    vatId: '',
                    description: '',
                    amount: 0.00,
                    amountAfterVAT: 0.00,
                    vatAmount: 0.00,
                    expenseAccountId: ''
                },
                summary: {
                    item: 0,
                    total: 0,
                    vatAmount: 0,
                    amountAfterVAT: 0,
                },
                addItem: false,
                IsExpenseAccount: false,
                tableLength: 0,
                cardLength: 0,
                currency: '',
                isExpenses: true,
                isExpensesDisable: false,
            }
        },
        
        filters: {
            roundOffFilter: function (value) {

                return parseFloat(value).toFixed(3).slice(0, -1).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,");
            }
        },
        watch: {
           
        },
        computed: {


            totalAmount: function () {
                var total = 0;
                if (this.dailyExpenseRow !== null && this.dailyExpenseRow.length > 0) {
                    this.dailyExpenseRow.forEach(function (dailyExpenses) {

                        total = parseFloat(total) + parseFloat(dailyExpenses.amount);

                    })
                }

                return total;
            },
        },

        methods: {
            GetAmountOfSelected: function () {
                
                return this.summary.amountAfterVAT;
            },
            updateLineTotal: function (e, prop, expense) {
                
                
                if (prop == "vat") {
                    if (e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        expense.vatId = e;
                    }
                }

                if (prop == "taxMehtod") {
                    if (e == '' || e == undefined) {
                        e = '';
                    }
                    else if (e == 'Exempted' || e == 'معفى') {
                        expense.vatId = null;
                    }
                    else {
                        expense.taxMehtod = e;
                    }
                }
                if (prop == "amount") {
                    if (e < 0 || e == '' || e == undefined) {
                        e = '';
                    }
                    else {
                        expense.amount = e;
                    }
                }

                if (expense.vatId != null && expense.vatId != '' && expense.vatId != undefined) {


                    var tax = this.options.find((value) => value.id == expense.vatId);

                    if (expense.taxMethod == 'Inclusive' || expense.taxMethod == 'شامل') {
                        expense.vatAmount = parseFloat(((expense.amount * tax.rate) / (100 + tax.rate)).toFixed(3).slice(0, -1));
                        expense.amountAfterVAT = parseFloat(expense.amount)
                    }
                    else if (expense.taxMethod == 'Exclusive' || expense.taxMethod == 'غير شامل') {
                        expense.vatAmount = ((expense.amount * tax.rate) / (100)).toFixed(3).slice(0, -1);
                        expense.amountAfterVAT = parseFloat(expense.amount) + parseFloat(expense.vatAmount)

                    }
                    else if (expense.taxMethod == 'Exempted' || expense.taxMethod == 'معفى') {
                        expense.vatAmount = 0;
                        expense.amountAfterVAT = parseFloat(expense.amount).toFixed(3).slice(0, -1);
                        this.randerTaxRate++
                    }
                }
                else if (expense.taxMethod == 'Exempted' || expense.taxMethod == 'معفى') {
                    expense.vatAmount = 0;
                    expense.amountAfterVAT = parseFloat(expense.amount).toFixed(3).slice(0, -1);
                    this.randerTaxRate++
                }

                this.dailyExpenseRow['expense'] = expense

                this.calcuateSummary();

                
                this.$emit('update:modelValue', this.dailyExpenseRow, this.IsExpenseAccount ? this.summary.amountAfterVAT : this.summary.total);

            },
            calcuateSummary: function () {
                this.summary.item = this.dailyExpenseRow.length;

                this.summary.total = this.dailyExpenseRow
                    .reduce((total, prod) => total + parseFloat(prod.amount), 0)
                    .toFixed(3).slice(0, -1);
                if (this.IsExpenseAccount) {
                    this.summary.vatAmount = this.dailyExpenseRow
                        .reduce((total, prod) => total + parseFloat(prod.vatAmount == undefined ? 0 : prod.vatAmount), 0)
                        .toFixed(3).slice(0, -1);
                    this.summary.amountAfterVAT = this.dailyExpenseRow
                        .reduce((total, prod) => total + parseFloat(prod.amountAfterVAT == undefined ? 0 : prod.amountAfterVAT), 0)
                        .toFixed(3).slice(0, -1);

                }
            },



            checkTableWidth: function () {
                if (document.getElementsByClassName('itemtable')[0] != undefined) {
                    this.tableLength = document.getElementsByClassName('itemtable')[0].clientWidth;
                    this.cardLength = document.getElementsByClassName('card')[0].clientWidth - 25;
                }
            },
            
            addDailyExpense: function () {

                this.dailyExpenseRow.push({
                    id: this.createUUID(),
                    amount: this.dailyExpenses.amount,
                    amountAfterVAT: this.dailyExpenses.amountAfterVAT,
                    vatAmount: this.dailyExpenses.vatAmount,
                    expenseAccountId: this.dailyExpenses.expenseAccountId,
                    description: this.dailyExpenses.description,
                    taxMethod: this.dailyExpenses.taxMethod,
                    vatId: this.dailyExpenses.vatId,
                });

                this.dailyExpenses = {
                    id: '',
                    amount: 0.00,
                    amountAfterVAT: 0.00,
                    vatAmount: 0.00,
                    description: '',
                    vatId: '',
                    expenseAccountId: '',
                };
                this.calcuateSummary();
                this.refresh += 1;
                this.loading = false;

            },
            createUUID: function () {
                var dt = new Date().getTime();
                var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (dt + Math.random() * 16) % 16 | 0;
                    dt = Math.floor(dt / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
                return uuid;
            },
            addExpense: function () {
                this.addItem = true;
                this.checkTableWidth();

                this.dailyExpenses = {

                    description: '',
                    taxMethod: '',
                    vatId: '',
                    amount: 0.00,
                    amountAfterVAT: 0.00,
                    vatAmount: 0.00,

                }
            },

            removeExpense: function (id) {

                var ds = this.dailyExpenseRow.findIndex(function (i) {
                    return i.id === id;
                });

                this.dailyExpenseRow.splice(ds, 1);
            },
            VatData: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/Product/TaxRateList?isActive=true', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        response.data.taxRates.forEach(function (result) {

                            if (root.value == result.id && root.value != undefined) {
                                root.selectedValue.push({
                                    id: result.id,
                                    name: (root.$i18n.locale == 'en' ) ? ((result.name != '' && result.name != null) ? result.name : result.nameArabic) + "(" + result.rate + "%)" : ((result.nameArabic != '' && result.nameArabic != null) ? result.nameArabic : result.name) + "(" + result.rate + "%)"
                                });
                            }

                            root.options.push({
                                id: result.id,
                                rate: result.rate,
                                name: (root.$i18n.locale == 'en' ) ? ((result.name != '' && result.name != null) ? result.code + ' ' + result.name : result.code + ' ' + result.nameArabic) + "(" + result.rate + "%)" : ((result.nameArabic != '' && result.nameArabic != null) ? result.code + ' ' + result.nameArabic : result.code + ' ' + result.name) + "(" + result.rate + "%)"
                            })
                        })
                    }
                }).then(function () {
                    if(root.$route.query.formName == "ExpenseBills")
                    {
                        var expenseDetailsList = root.$store.isExpenseBillItemsList;
                                    
                        root.isExpenses = false;
                        root.isExpensesDisable = true;
                        expenseDetailsList.purchaseBillItems.forEach(x => {
                            root.dailyExpenses.expenseAccountId = x.accountId;
                            root.dailyExpenses.amount = expenseDetailsList.totalAmount;
                            root.dailyExpenses.description = x.description;
                        });
                        root.refresh++;
                    }
                    else
                    {
                        if (root.$route.query.data != undefined) 
                        { 
                            if (!root.$route.query.data.isTemporaryCashIssue) {
                                root.dailyExpenseRow = root.$route.query.data.dailyExpenseDetails;
                                for (var k = 0; k < root.dailyExpenseRow.length; k++) {
                                    root.updateLineTotal(root.dailyExpenseRow[k].vatId, "vat", root.dailyExpenseRow[k]);
                                    root.calcuateSummary()
                                }
                            }
                        }
                    }
                });
            },

        },
        created: function () {
        
            var root = this;
            
            if (root.formName == 'dailyexpense') {

                root.IsExpenseAccount = false;
            }
            else {
                root.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
            }

            if (root.$route.query.data != undefined) {
                
                if (!root.$route.query.data.isTemporaryCashIssue) {

                    if (root.IsExpenseAccount) {
                        root.VatData();
                    }
                    else {
                          
                           
                            if(root.$route.query.formName == "ExpenseBills")
                            {
                                var expenseDetailsList = root.$store.isExpenseBillItemsList;
                                expenseDetailsList.purchaseBillItems.forEach(x => {
                                    root.dailyExpenses.amount = expenseDetailsList.totalAmount;
                                    root.dailyExpenses.description = x.description;
                                    root.dailyExpenses.total = root.dailyExpenses.amount;
                                    root.updateLineTotal(root.dailyExpenses.amount, 'amount', root.dailyExpenses);
                                });
                                root.refresh++;
                            }
                            else
                            {
                                root.dailyExpenseRow = root.$route.query.data.dailyExpenseDetails;
                                this.calcuateSummary();
                            }
                            
                        
                    }
                }
            }
        },
        updated: function () {

            document.querySelector("html").classList.remove("perfect-scrollbar-on");
            this.$emit('updatedailyExpenseRows', this.dailyExpenseRow, this.IsExpenseAccount ? this.summary.amountAfterVAT : this.summary.total);
        },
        mounted: function () {
            
            if ((this.$i18n.locale == 'en')) {
                this.options1 = ['Inclusive', 'Exclusive', 'Exempted'];
            }


            else if ((this.$i18n.locale == 'ar')) {
                this.options1 = ['شامل', 'غير شامل', 'معفى'];
            }
            else  {
                this.options1 = ['Inclusive', 'Exclusive', 'Exempted'];
            }
            this.currency = localStorage.getItem('currency');
            if (this.formName == 'dailyexpense') {

                this.IsExpenseAccount = false;
            }
            else {
                this.IsExpenseAccount = localStorage.getItem('IsExpenseAccount') == 'true' ? true : false;
            }

            if (this.IsExpenseAccount) {
                this.VatData();

            }

            this.addItem = this.dailyExpenseRow.length > 0 ? false : true;

        }
    }</script>