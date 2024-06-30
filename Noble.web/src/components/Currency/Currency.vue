<template>
    <div class="row" v-if="isValid('CanViewCurrency')">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('TheCurrency.Currency') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('TheCurrency.Home')
                                    }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('TheCurrency.Currency') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddCurrency')" v-on:click="AddCurrency" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('TheCurrency.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('TheCurrency.Close') }}
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
                                <button class="btn btn-secondary" type="button" id="button-addon1"><i
                                        class="fas fa-search"></i></button>
                                <input v-model="searchQuery" type="text" class="form-control"
                                    :placeholder="$t('TheCurrency.SearchByCurrency')"
                                    aria-label="Example text with button addon" aria-describedby="button-addon1">
                            </div>
                        </div>
                        <div class=" col-lg-4 mt-1" v-if="!advanceFilters">

                            <button v-on:click="search22(true)" :disabled ="!searchQuery" type="button" class="btn btn-outline-primary mt-3">
                                {{ $t('Sale.ApplyFilter') }}
                            </button>
                            <button v-on:click="clearData(false)" :disabled ="!searchQuery" type="button" class="btn btn-outline-primary mx-2 mt-3">
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
                                    <th>
                                        #
                                    </th>
                                    <th class="text-center">
                                        {{ $t('TheCurrency.Image') }}
                                    </th>
                                    <th v-if="english == 'true'">
                                        {{$englishLanguage($t('TheCurrency.CurrencyName'))   }}
                                    </th>
                                    <th v-if="isOtherLang()">
                                        {{$arabicLanguage($t('TheCurrency.CurrencyName'))   }}
                                    </th>
                                    <th>
                                        {{ $t('TheCurrency.SIGN') }}
                                    </th>
                                    <th>
                                        {{ $t('TheCurrency.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(details, index) in resultQuery" v-bind:key="details.id">

                                    <td>
                                        {{ index + 1 }}
                                    </td>
                                    <td v-if="details.image == null || details.image == '' || details.image == undefined"
                                        class="text-center">
                                        <img src="images/Product.png" style="width: 50px;" />
                                    </td>
                                    <td v-else class="text-center">
                                        <img :src="'data:image/png;base64,' + details.image" style="width: 50px;" />
                                    </td>

                                    <td v-if="english == 'true'">
                                        <strong v-if="index == 0">
                                            <a href="javascript:void(0)" v-if="isValid('CanEditCurrency')"
                                                v-on:click="EditCurrency(details.id, true)">{{ details.name }}</a>
                                            <span v-else>{{ details.name }}</span>
                                        </strong>
                                        <strong v-else>
                                            <a href="javascript:void(0)" v-if="isValid('CanEditCurrency')"
                                                v-on:click="EditCurrency(details.id, false)">{{ details.name }}</a>
                                            <span v-else>{{ details.name }}</span>
                                        </strong>
                                    </td>
                                    <td v-if="isOtherLang()">
                                        <strong v-if="index == 0">
                                            <a href="javascript:void(0)" v-if="isValid('CanEditCurrency')"
                                                v-on:click="EditCurrency(details.id, true)">{{ details.nameArabic }}</a>
                                            <span v-else>{{ details.nameArabic }}</span>
                                        </strong>
                                        <strong v-else style="color :red !important">
                                            <a href="javascript:void(0)" v-if="isValid('CanEditCurrency')"
                                                v-on:click="EditCurrency(details.id)">{{ details.nameArabic }}</a>
                                            <span v-else>{{ details.nameArabic }}</span>
                                        </strong>

                                    </td>
                                    <td>
                                        {{ details.sign }}
                                    </td>
                                    <td>
                                        <span v-if="details.isActive" class="badge badge-boxed  badge-outline-success">{{
                                            $t('TheCurrency.Active') }}</span>
                                        <span v-else class="badge badge-boxed  badge-outline-danger">{{
                                            $t('TheCurrency.InActive') }}</span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>

            <currencymodel :newcurrency="newCurrency" :show="show" v-if="show" @close="show = false" :type="type" />
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
    name: 'Currency',
    data: function () {
        return {
            advanceFilters: false,
            arabic: '',
            english: '',
            show: false,
            searchQuery: '',
            currencylist: [

            ],
            newCurrency: {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                sign: '',
                arabicSign: '',
                image: '',
                isActive: true,
                isDisable: false,
            },
            type: '',

        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return this.currencylist.filter((cur) => {
                    return root.searchQuery.toLowerCase().split(' ').every(v => cur.name.toLowerCase().includes(v) || cur.nameArabic.toLowerCase().includes(v))
                })
            } else {
                return root.currencylist;
            }
        },
    },
    search22: function () {
    this.GetCurrencyData(this.searchQuery, this.currentPage);
            },

            clearData: function () {
                this.searchQuery="";
                this.GetCurrencyData(this.searchQuery, this.currentPage);

            },
    // watch: {
    //     search: function (val) {
    //         this.GetCurrencyData(val, 1);
    //     }
    // },
    methods: {
        GotoPage: function (link) {
            this.$router.push({ path: link });
        },
        AddCurrency: function () {
            this.newCurrency = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                sign: '',
                arabicSign: '',
                image: '',
                isActive: true,
                isDisable: false,
            };
            this.show = !this.show;
            this.type = "Add";
        },
        GetCurrencyData: function () {
            var root = this;
            var url = '/Product/CurrencyList';
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.currencylist = response.data.currencies;
                }
            });
        },
        EditCurrency: function (id, isDisable) {

            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Product/CurrencyDetail?Id=' + id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {
                        root.newCurrency.id = response.data.id;
                        root.newCurrency.name = response.data.name;
                        root.newCurrency.nameArabic = response.data.nameArabic;
                        root.newCurrency.sign = response.data.sign;
                        root.newCurrency.isDisable = isDisable;
                        root.newCurrency.arabicSign = response.data.arabicSign;
                        root.newCurrency.image = response.data.image;
                        root.newCurrency.isActive = response.data.isActive;
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
        this.GetCurrencyData();
    }
}
</script>