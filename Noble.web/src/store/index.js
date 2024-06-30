/*import { createStore } from 'vuex'*/
import { defineStore } from 'pinia'

export const useMainStore = defineStore('main', {
    state: () => ({
        isLoggedIn: false,
        isLockOut: false,
        allowanceTypeList:[],
        processList: [],
        userRolesList: [],
        rolesList: [],
        companyList: [],
        terminalList: [],
        accounts: [],
        brandList: [],
        priceRecordList: [],
        priceLabelingRecordList: [],
        expenseBillItemsList: [],
        editObj:{},
        taxRateList:[],
        productQuantityList:[],


    }),
    getters: {
        // Define your getters here
        isAuthenticated: (state) => state.isLoggedIn,
        isLockAuthenticate: (state) => state.isLockOut,
        isProcessList: (state) => state.processList,
        isAllowanceTypeList: (state) => state.allowanceTypeList,
        isUserRolesList: (state) => state.userRolesList,
        isRolesList: (state) => state.rolesList,
        isCompanyList: (state) => state.companyList,
        isterminalList: (state) => state.terminalList,
        isaccounts: (state) => state.accounts,
        isbrandList: (state) => state.brandList,
        ispriceRecordList: (state) => state.priceRecordList,
        isPriceLabelingRecordList: (state) => state.priceLabelingRecordList,
        isExpenseBillItemsList: (state) => state.expenseBillItemsList,
        isGetEdit: (state) => state.editObj,
        getTaxRateList: (state) => state.taxRateList,
        isProductQuantityList: (state) => state.productQuantityList,
       

    },
    actions: {
        login(isLoggedIn) {
            this.isLoggedIn = isLoggedIn;
            this.isLockOut = isLoggedIn;
            
        },
        lockOut() {
            this.isLockOut= false;
            this.allowanceTypeList=[];
            this.processList= [];
            this.userRolesList= [];
            this.rolesList= [];
            this.companyList= [];
            this.terminalList= [];
            this.accounts= [];
            this.brandList= [];
            this.priceRecordList= [];
            this.priceLabelingRecordList= [];
            this.expenseBillItemsList= [];
            this.editObj={};
            this.taxRateList=[];
            this.productQuantityList=[];
        },
        logout() {
            this.isLockOut= true;
            this.isLoggedIn= false;
            this.allowanceTypeList=[];
            this.processList= [];
            this.userRolesList= [];
            this.rolesList= [];
            this.companyList= [];
            this.terminalList= [];
            this.accounts= [];
            this.brandList= [];
            this.priceRecordList= [];
            this.priceLabelingRecordList= [];
            this.expenseBillItemsList= [];
            this.editObj={};
            this.taxRateList=[];
            this.productQuantityList=[];
        },
        GetAllowanceTypeList(allowanceTypeList) {
            this.allowanceTypeList = allowanceTypeList;
        },
        GetProcessList(processList) {
            this.processList = processList;
        },
        GetUserRolesList(userRolesList) {
            this.userRolesList = userRolesList;
        },
        GetRoleList(rolesList) {
            this.rolesList = rolesList;
        },
        GetComapanyList(companyList) {
            this.companyList = companyList;
        },
        GetTerminalList(terminalList) 
        {
            this.terminalList = terminalList;
        },
        GetAccountList(accounts) {
            this.accounts = accounts;
        },
        GetBrandList(brandList) {
            this.brandList = brandList;
        },
        GetPriceRecordList(priceRecordList) {
           this.priceRecordList = priceRecordList;
        },
        GetPriceLabelingRecord(priceLabelingRecordList) {
            this.priceLabelingRecordList = priceLabelingRecordList;
        },
        GetExpenseBillItemsList(expenseBillItemsList) {
            this.expenseBillItemsList = expenseBillItemsList;
        },
        GetEditObj(obj) {
            this.editObj = obj;
        },
        SetTaxRateList(taxRateList) {
            this.taxRateList = taxRateList;
        },
        
    },
    persist: true,
});
