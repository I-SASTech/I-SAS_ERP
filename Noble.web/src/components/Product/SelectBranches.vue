<template>
    <modal :show="show" >
        <div class="modal-content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h6 class="modal-title m-0" id="myModalLabel">Select Branches</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row ">
                                <div class="form-group has-label col-sm-12 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddBranchUsers.Branch') }}:<span class="text-danger"> *</span></label>
                                    <branch-dropdown v-model="brancheslist" :ismultiple="true" />
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer ">
                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveBranch" > Save</button>
                            <button type="button" class="btn btn-danger btn-sm" v-on:click="close()">{{ $t('AddBranches.Cancel') }}</button>
                        </div>
                        
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    

    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },
        props: ['show'],
        data: function () {
            return {
                loading: false,
                brancheslist:[]
            }
        },
      
        methods: {
            close: function () {
                this.$emit('close');
            },
            SaveBranch: function () {
                this.$emit('save',this.brancheslist);
            },
        },
        mounted: function () {

        }
    }
</script>
