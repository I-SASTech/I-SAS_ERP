<template>
    <div>
        <multiselect v-model="DisplayValue" v-if="disable" :options="options" :multiple="false" track-by="name"
            :clear-on-select="false" :show-labels="false" label="name" disabled
            v-bind:placeholder="$t('AccountNumberDropdown.Search')" :preselect-first="true">

            <template v-slot:noResult>
                <a class="btn btn-primary " v-on:click="AddCustomer()" v-if="isValid('CanAddSupplier')">{{
                    $t('SupplierDropdown.CreateQuickSupplier') }}</a><br />
            </template>
        </multiselect>
        <multiselect v-model="DisplayValue" v-else :options="options" :multiple="false" track-by="name"
            :clear-on-select="false" :show-labels="false" label="name"
            v-bind:placeholder="$t('AccountNumberDropdown.Search')"
            v-bind:class="($i18n.locale == 'en' || isLeftToRight()) ? 'text-left ' : 'arabicLanguage '"
            :preselect-first="true">
            <template v-slot:noResult>
                <a class="btn btn-primary " v-on:click="AddCustomer()" v-if="isValid('CanAddSupplier')">{{
                    $t('SupplierDropdown.CreateQuickSupplier') }}</a><br />
            </template>
        </multiselect>






    </div>
</template>
<script>
import clickMixin from '@/Mixins/clickMixin'
import Multiselect from "vue-multiselect";
export default {
    name: "SupplierDropdown",
    props: ["modelValue", "status", 'disable', "paymentTerm", 'isCustomer'],
    mixins: [clickMixin],
    components: {
        Multiselect,
    },

    data: function () {
        return {
            loading: false,
            paymentTerms: '',
            isRaw: '',
            arabic: '',
            english: '',
            options: [],
            value: "",
            show: false,

        };
    },
    methods: {


        getData: function () {
            var root = this;
            var token = "";
            if (token == '') {
                token = localStorage.getItem("token");
            }


            var supplier = this.status == undefined ? false : this.status;
            this.paymentTerms = this.paymentTerm ? 'Credit' : '';

            this.$https
                .get('/Contact/ContactList?IsDropDown=' + true + '&isCustomer=' + this.isCustomer + '&isActive=' + true + '&status=' + supplier + '&paymentTerms=' + this.paymentTerms, {
                    headers: { Authorization: `Bearer ${token}` },
                })
                .then(function (response) {


                    if (response.data != null) {
                        response.data.results.forEach(function (sup) {
                            if (sup.contactNo1 == null)
                                sup.contactNo1 = '';
                            if (sup.customerCode == null)
                                sup.customerCode = '';
                            var displayName = '';
                            if (sup.customerDisplayName != null && sup.customerDisplayName != "") {
                                displayName = '\u202A' + sup.customerDisplayName + '\u202C' + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + sup.contactNo1 + sup.customerCode;
                            } else if (sup.englishName != '' && sup.englishName != null) {
                                displayName = sup.englishName + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + sup.contactNo1 + sup.customerCode;
                            } else if (sup.arabicName != '' && sup.arabicName != null) {
                                displayName = '\u202A' + sup.arabicName + '\u202C' + '\xa0\xa0\xa0\xa0\xa0\xa0\xa0' + sup.contactNo1 + sup.customerCode;
                            } else {
                                displayName = sup.englishName + sup.customerCode;
                            }
                            root.options.push({
                                id: sup.id,
                                name: displayName,
                                accountId: sup.accountId
                            });
                        });
                    }
                })
                .then(function () {
                    root.value = root.options.find(function (x) {
                        return x.accountId == root.modelValue;
                    });
                });
        },
    },


    computed: {
        DisplayValue: {
            get: function () {
                if (this.value != "" || this.value != undefined) {
                    return this.value;
                }
                return this.modelValue;
            },
            set: function (value) {
                if (value != null) {
                    this.value = value;
                    this.$emit("update:modelValue", value.accountId);
                }
                else {
                    this.value = value;
                    this.$emit("update:modelValue", null);
                }
            },
        },
    },
    mounted: function () {
        this.english = localStorage.getItem('English');
        this.arabic = localStorage.getItem('Arabic');
        this.isRaw = localStorage.getItem('IsProduction');
        this.getData();
    },
};
</script>