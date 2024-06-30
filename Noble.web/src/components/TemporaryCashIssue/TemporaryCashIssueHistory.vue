<template>
    <modal :show="show" :modalLarge="true">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h5 class="modal-title DayHeading" id="myModalLabel">{{ $t('TemporaryCashIssueHistory.TemporaryCashIssueHistory') }} </h5>

                        </div>

                        <div class="">
                            <div class="card-body">
                                <div class="row ">
                                    <div class="col-lg-12 ">
                                        <table class="table table-shopping" style="table-layout:fixed;width:100%;" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                            <thead class="">
                                                <tr>
                                                    <th>
                                                        #
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssueHistory.DocumentNo') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssueHistory.Date') }}
                                                    </th>
                                                    <th>
                                                        {{ $t('TemporaryCashIssueHistory.Amount') }}
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="(details,index) in temporaryCashHistory" v-bind:key="index">
                                                    <td>
                                                        {{index+1}}
                                                    </td>
                                                    <td>
                                                        {{details.documentNo}}
                                                    </td>
                                                    <td>
                                                        {{details.date}}
                                                    </td>
                                                    <td>
                                                        {{currency}}  {{parseFloat(details.amount).toFixed(2).replace(/(\d)(?=(\d{3})+(?:\.\d+)?$)/g, "$1,")}}
                                                    </td>

                                                </tr>
                                            </tbody>

                                        </table>

                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('ActionModel.btnClear') }}</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    export default {
        props: ['show', 'temporaryCash'],
        mixins: [clickMixin],
        data: function () {
            return {
                render: 0,
                currency: '',
                temporaryCashHistory:[]
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },
        },
        created: function () {
            

            this.temporaryCashHistory = this.temporaryCash;            
        },
        mounted: function () {
            this.currency = localStorage.getItem('currency');
        }
    }
</script>
