<template>
    <div class="row" v-if="isValid('CanViewAllowanceType')">

        <div class="col-lg-12">
            <div class="row">
                <div class="col-sm-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title">{{ $t('AllowanceType.AllowanceType') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AllowanceType.Home') }}</a></li>
                                    <li class="breadcrumb-item active">{{ $t('AllowanceType.AllowanceType') }}</li>
                                </ol>
                            </div>
                            <div class="col-auto align-self-center">
                                <a v-if="isValid('CanAddAllowanceType')" v-on:click="openmodel"
                                    href="javascript:void(0);" class="btn btn-sm btn-outline-primary mx-1">
                                    <i class="align-self-center icon-xs ti-plus"></i>
                                    {{ $t('AllowanceType.AddNew') }}
                                </a>
                                <a v-on:click="GotoPage('/StartScreen')" href="javascript:void(0);"
                                    class="btn btn-sm btn-outline-danger">
                                    {{ $t('AllowanceType.Close') }}
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
                        <input v-model="searchQuery" type="text" class="form-control"
                            :placeholder="$t('AllowanceType.Search')" aria-label="Example text with button addon"
                            aria-describedby="button-addon1">
                    </div>
                </div>
                <div class="card-body">
                    <div class="table-responsive">
                        <table class="table mb-0">
                            <thead class="thead-light table-hover">
                                <tr>
                                    <th>#</th>

                                    <th v-if="english == 'true'" class="text-center">
                                        {{ $t('AllowanceType.NameEnglish') }}
                                    </th>
                                    <th v-if="isOtherLang()" class="text-center">
                                        {{ $t('AllowanceType.NameArabic') }}
                                    </th>
                                    <th class="text-center" width="40%">
                                        {{ $t('AllowanceType.Description') }}
                                    </th>
                                    <th>
                                        {{ $t('AllowanceType.Status') }}
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(allowanceType, index) in resultQuery" v-bind:key="allowanceType.id">
                                    <td>
                                        {{ index + 1 }}
                                    </td>

                                    <td class="text-center" v-if="isValid('CanEditAllowanceType')">
                                        <strong>
                                            <a href="javascript:void(0)"
                                                v-on:click="EditAllowanceType(allowanceType.id)">
                                                {{ allowanceType.name }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center" v-else>
                                        <strong>
                                            {{ allowanceType.name }}
                                        </strong>
                                    </td>
                                    <td class="text-center" v-if="isValid('CanEditAllowanceType')">
                                        <strong>
                                            <a href="javascript:void(0)"
                                                v-on:click="EditAllowanceType(allowanceType.id)">
                                                {{ allowanceType.nameArabic }}</a>
                                        </strong>
                                    </td>
                                    <td class="text-center" v-else>
                                        <strong>
                                            {{ allowanceType.nameArabic }}
                                        </strong>
                                    </td>

                                    <td class="text-center">
                                        {{ allowanceType.description }}
                                    </td>
                                    <td>
                                        <span v-if="allowanceType.isActive"
                                              class="badge badge-boxed  badge-outline-success"> {{ $t('AllowanceType.Active') }}</span>
                                        <span v-else
                                              class="badge badge-boxed  badge-outline-danger"> {{ $t('AllowanceType.De-Active') }}</span>
                                    </td>





                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <hr />

                </div>
            </div>

            <AddAllowanceType :newAllowanceType="newAllowanceType" :show="show" v-if="show" @close="close" :type="type" />
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
            arabic: '',
            english: '',
            searchQuery: '',
            show: false,
            allowanceTypelist: [],
            newAllowanceType: {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                description: '',
                isActive: true
            },
            type: '',
        }
    },
    computed: {
        resultQuery: function () {
            var root = this;
            if (this.searchQuery) {
                return root.allowanceTypelist.filter((allowanceType) => {

                    return root.searchQuery.toLowerCase().split(' ').every(v => allowanceType.name.toLowerCase().includes(v) || allowanceType.nameArabic.toLowerCase().includes(v))
                })
            } else {
                return root.allowanceTypelist;
            }
        },
    }, methods: {
        GotoPage: function (link) {
                this.$router.push({path: link});
            },
        close: function () {

            this.show = false;
            this.GetAllowanceTypeData();
        },
        openmodel: function () {
            this.newAllowanceType = {
                id: '00000000-0000-0000-0000-000000000000',
                name: '',
                nameArabic: '',
                description: '',
                isActive: true
            }
            this.show = !this.show;
            this.type = "Add";
        },
        GetAllowanceTypeData: function () {
            var root = this;
            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('Payroll/AllowanceTypeList?isDropdown=false', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                if (response.data != null) {
                    root.$store.GetAllowanceTypeList(response.data.allowanceTypes);
                    root.allowanceTypelist = response.data.allowanceTypes;
                }
            });
        },

        EditAllowanceType: function (Id) {


            var root = this;

            var token = '';
            if (token == '') {
                token = localStorage.getItem('token');
            }
            root.$https.get('/Payroll/AllowanceTypeDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } })
                .then(function (response) {
                    if (response.data) {

                        root.newAllowanceType.id = response.data.id;
                        root.newAllowanceType.description = response.data.description;
                        root.newAllowanceType.name = response.data.name;
                        root.newAllowanceType.nameArabic = response.data.nameArabic;
                        root.newAllowanceType.isActive = response.data.isActive;
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
        this.GetAllowanceTypeData();
    }
}
</script>